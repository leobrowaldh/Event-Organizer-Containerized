using Data.DataAccess;
using Data.Models;
using Data.ColorList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Event_Organizer.web.Pages
{
    public class UserSelectModel(IDataAccess injectedDataAccess) : PageModel
    {
        private readonly IDataAccess _dataAccess = injectedDataAccess;

        [BindProperty(SupportsGet = true)]
        public Guid EventId { get; set; }

        public ICollection<User>? Users { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "You must enter a name.")]
        [MinLength(1, ErrorMessage = "Name must have at least one character.")]
        public string? NewUser { get; set; }

        public void OnGet()
        {
            Users = _dataAccess.GetEventUsers(EventId);
        }

        public IActionResult OnPost()
        {
            // Always reload the list of users so they appear after a post request
            Users = _dataAccess.GetEventUsers(EventId);

            // Check if the form input is valid
            if (!ModelState.IsValid)
            {
                // If validation fails, return the page with error messages and the updated user list
                return Page();
            }

            // Check if the name already exists in the user list
            if (Users.Any(u => u.Name.Equals(NewUser, StringComparison.OrdinalIgnoreCase)))
            {
                // Add an error to the model state if the name is already taken
                ModelState.AddModelError("", "This name has already been submitted.");

                // Return the page with the error message and updated user list
                return Page();
            }

            // Create a new User object and set the Name property to the value of NewUser
            User userToAdd = new User() { Name = NewUser };

            // Assign a random color from the ColorList
            Random rand = new Random();
            int randomIndex = rand.Next(ColorList.Colors.Count);
            userToAdd.VotingColor = ColorList.Colors[randomIndex];

            // Get the current Event object using the GetEvent method of the IDataAccess interface
            Event? currentEvent = _dataAccess.GetEvent(EventId);

            // If the currentEvent is not null, set the Event property of the userToAdd object to the currentEvent
            if (currentEvent != null)
            {
                userToAdd.Event = currentEvent;
            }

            // Call the PostUser method of the IDataAccess interface to add the userToAdd object to the data source
            _dataAccess.PostUser(userToAdd);

            // Redirect to the UserSelect page with the eventId parameter
            return RedirectToPage("/UserSelect", new { eventId = EventId });
        }

        // New method to handle user name clicks
        public IActionResult OnPostSelectUser(int userId)
        {
            // Store the userId in session
            HttpContext.Session.SetInt32("UserId", userId);

            // Redirect to the event page or wherever appropriate
            return RedirectToPage("/Event", new { eventId = EventId });
        }

        // Helper method to get the current user's ID from session
        public int? GetCurrentUserId()
        {
            // Retrieve userId from session
            return HttpContext.Session.GetInt32("UserId");
        }
    }
}
