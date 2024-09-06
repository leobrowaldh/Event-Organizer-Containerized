using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Event_Organizer.web.Pages
{
    public class EventModel(IDataAccess injectedDataAccess) : PageModel
    {
        private readonly IDataAccess _dataAccess = injectedDataAccess;

        [BindProperty(SupportsGet = true)]
        public Guid EventId { get; set; }

		[BindProperty(SupportsGet = true)]
		public string ActivityName { get; set; }

        public Event? ActiveEvent { get; set; }

        public ICollection<Activity> Activities { get; set; } = [];

		// Get the current user's ID from the session
        public int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        public IActionResult OnGet()
        {
			ActiveEvent = _dataAccess.GetEvent(EventId);
			Activities = _dataAccess.GetEventActivities(EventId);

            if (CurrentUserId == null)
            {
                // If there's no user in session, redirect them to the UserSelect page
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }
            return Page();
        }

		public IActionResult OnPost()
        {
            // Ensure the user is set in the session
            if (CurrentUserId == null)
            {
                // If there's no user in session, redirect them to the UserSelect page
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }

            ActiveEvent = _dataAccess.GetEvent(EventId);
            if (ActiveEvent != null && !string.IsNullOrEmpty(ActivityName))
            {
                // Create and add the new activity to the event
                Activity newActivity = new Activity()
                {
                    Name = ActivityName,
                    Event = ActiveEvent
                };

                _dataAccess.PostActivity(newActivity);
            }

            return RedirectToPage("/Event", new { eventId = EventId });
        }

	}
}
