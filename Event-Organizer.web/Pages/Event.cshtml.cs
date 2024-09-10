using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Event_Organizer.web.Pages
{
    public class EventModel : PageModel
    {
        public ICollection<User>? Users { get; set; }

        private readonly IDataAccess _dataAccess;

        public EventModel(IDataAccess injectedDataAccess)
        {
            _dataAccess = injectedDataAccess;
        }

        [BindProperty(SupportsGet = true)]
        public Guid EventId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ActivityName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Description { get; set; }

        [BindProperty]
        public string UpdatedActivityName { get; set; }

        [BindProperty]
        public string UpdatedDescription { get; set; }

        [BindProperty]
        public int ActivityId { get; set; }

        public Event? ActiveEvent { get; set; }

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        // Get the current user's ID from the session
        public User? CurrentUser { get; set; }
        public int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        public IActionResult OnGet()
        {
            Users = _dataAccess.GetEventUsers(EventId);
            ActiveEvent = _dataAccess.GetEvent(EventId);
			Activities = _dataAccess.GetEventActivities(EventId);

            if (CurrentUserId.HasValue)
            {
                CurrentUser = _dataAccess.GetUser(CurrentUserId.Value);
            }

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
                    EventId = EventId, // Set the EventId directly
                    Description = Description
                };

                _dataAccess.PostActivity(newActivity);
            }

            return RedirectToPage("/Event", new { eventId = EventId });
        }

        // Handle adding a new activity with default values
        public IActionResult OnPostAddNewActivity()
        {
            if (CurrentUserId == null)
            {
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }

            // Create a new activity with default values
            Activity newActivity = new Activity()
            {
                Name = "New Activity",
                Description = "Describe the activity...",
                EventId = EventId // Use the existing EventId
            };

            _dataAccess.PostActivity(newActivity);

            // Return the new activity data as JSON
            return new JsonResult(new
            {
                id = newActivity.Id,
                name = newActivity.Name,
                description = newActivity.Description,
                eventId = newActivity.EventId
            });
        }

        // Handle activity edits
        public IActionResult OnPostEditActivity()
        {
            if (CurrentUserId == null)
            {
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }

            // Fetch the activity to edit using the integer ActivityId
            var activity = _dataAccess.GetEventActivities(EventId)
                                      .FirstOrDefault(a => a.Id == ActivityId);

            if (activity != null)
            {
                // Update the activity fields with the new values
                activity.Name = UpdatedActivityName;
                activity.Description = UpdatedDescription;

                _dataAccess.UpdateActivity(activity);
            }

            return RedirectToPage("/Event", new { eventId = EventId });
        }

        // Handle activity deletion
        public IActionResult OnPostDeleteActivity(Guid eventId, int activityId)
        {
            if (CurrentUserId == null)
            {
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }

            var activity = _dataAccess.GetEventActivities(eventId)
                                      .FirstOrDefault(a => a.Id == activityId);

            if (activity != null)
            {
                _dataAccess.DeleteActivity(activity);
            }

            return RedirectToPage("/Event", new { eventId = eventId });
        }

    }
}
