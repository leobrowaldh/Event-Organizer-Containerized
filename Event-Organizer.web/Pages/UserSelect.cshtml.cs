using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

            // Redirect to the UserSelect page with the eventId parameter
            return RedirectToPage("/UserSelect", new { eventId = EventId });
        }
    }
}
