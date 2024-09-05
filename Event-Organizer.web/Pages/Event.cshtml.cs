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
        public int UserId { get; set; }
		[BindProperty(SupportsGet = true)]
		public string ActivityName { get; set; }
        public Event? ActiveEvent { get; set; }

        public ICollection<Activity> Activities { get; set; } = [];
        public void OnGet()
        {
			ActiveEvent = _dataAccess.GetEvent(EventId);
			Activities = _dataAccess.GetEventActivities(EventId);
		}

		public IActionResult OnPost()
        {
			ActiveEvent = _dataAccess.GetEvent(EventId);
			Activity  newActivity = new Activity() { Name = ActivityName };
            newActivity.Event = _dataAccess.GetEvent(EventId);

			_dataAccess.PostActivity(newActivity);

			return RedirectToPage("/Event", new { eventId = EventId, userId = UserId });
		}

	}
}
