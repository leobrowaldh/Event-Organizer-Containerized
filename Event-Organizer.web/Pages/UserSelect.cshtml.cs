using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System;

namespace Event_Organizer.web.Pages
{

    public class UserSelectModel(IDataAccess injectedDataAccess) : PageModel
    {
        private readonly IDataAccess _dataAccess = injectedDataAccess;
        [BindProperty(SupportsGet = true)]
        public Guid EventId { get; set; }
        public ICollection<User>? Users { get; set; }
        [BindProperty]
        public string? NewUser { get; set; }
        public void OnGet()
        {

            Users = _dataAccess.GetEventUsers(EventId);
        }
        public IActionResult OnPost()
        {
            // Create a new User object and set the Name property to the value of NewUser
            User userToAdd = new User() { Name = NewUser };

            // Get the current Event object using the GetEvent method of the IDataAccess interface
            Event? currentEvent = _dataAccess.GetEvent(EventId);

            // If the currentEvent is not null, set the Event property of the userToAdd object to the currentEvent
            if (currentEvent != null)
            {
                userToAdd.Event = currentEvent;
            }

            // Call the PostUser method of the IDataAccess interface to add the userToAdd object to the data source
            _dataAccess.PostUser(userToAdd);

            // Store the userId in session after adding the user
            HttpContext.Session.SetInt32("UserId", userToAdd.Id);

            // Redirect to the UserSelect page with the eventId parameter
            return RedirectToPage("/UserSelect", new { eventId = EventId });
        }

        // Helper method to get the current user's ID from session
        public int? GetCurrentUserId()
        {
            // Retrieve userId from session
            return HttpContext.Session.GetInt32("UserId");
        }
    }
}
