using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Event_Organizer.web.Pages
{
    public class EventModel : PageModel
    {
        public bool IsFirstTimeOnEventPage { get; set; }
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

        public ICollection<Activity> Activities { get; set; } = [];

        // Get the current user's ID from the session
        public User? CurrentUser { get; set; }
        public int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        public IActionResult OnGet()
        {
            // Check if the "IsFirstTimeOnSite" cookie exists
            var cookie = Request.Cookies["IsFirstTimeOnEventPage"];

            if (string.IsNullOrEmpty(cookie))
            {
                // If the cookie doesn't exist, it's the user's first visit
                IsFirstTimeOnEventPage = true;

                // Set the "FirstVisit" cookie with a value and expiration date
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1) // Set the cookie to expire in 1 year
                };
                Response.Cookies.Append("IsFirstTimeOnEventPage", "true", cookieOptions);
            }
            else
            {
                // If the cookie exists, it's not the first visit
                IsFirstTimeOnEventPage = false;
            }

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

		public IActionResult OnPostAddActivity()
        {
            ActiveEvent = _dataAccess.GetEvent(EventId);
            // Ensure the user is set in the session
            if (CurrentUserId == null)
            {
                // If there's no user in session, redirect them to the UserSelect page
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }

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

            var activity = _dataAccess.GetActivityWithUsers(activityId);

            if (activity != null)
            {
				var usersToUpdate = activity.Users.ToList(); // Convert to list to avoid modification during iteration

				// Update associated users to set ActivityId to null
				foreach (var user in usersToUpdate)
				{
					user.ActivityId = null; 
					_dataAccess.PutUser(user); 
				}

				_dataAccess.DeleteActivity(activity);

			}

            return RedirectToPage("/Event", new { eventId = eventId });
        }

        public IActionResult OnPostVote(int activityId)
        {
			ActiveEvent = _dataAccess.GetEvent(EventId);
			if (CurrentUserId == null)
            {
                return RedirectToPage("/UserSelect", new { eventId = EventId });
            }
            else
            {
                User? votingUser = _dataAccess.GetUser((int)CurrentUserId);
                Activity? votedActivity = _dataAccess.GetActivity(activityId);
                if (votedActivity != null && votingUser != null)
                {
                    votingUser.Activity = votedActivity;
                    _dataAccess.PutUser(votingUser);
                }
            }

			return RedirectToPage("/Event", new { eventId = EventId });
		}

	}
}
