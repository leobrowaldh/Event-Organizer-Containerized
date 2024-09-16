using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{
    public class EventResultsModel : PageModel
    {
        private readonly IDataAccess _dataAccess;

        public EventResultsModel(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Event? ActiveEvent { get; set; }
        public ICollection<ActivityResult> ActivityResults { get; set; } = new List<ActivityResult>();

        public void OnGet(Guid eventId)
        {
            ActiveEvent = _dataAccess.GetEvent(eventId);

            if (ActiveEvent != null)
            {
                var activities = _dataAccess.GetEventActivities(eventId);
                foreach (var activity in activities)
                {
                    var voteCount = activity.Users.Count;
                    ActivityResults.Add(new ActivityResult
                    {
                        Activity = activity,
                        VoteCount = voteCount
                    });
                }
            }
        }

        public class ActivityResult
        {
            public Activity Activity { get; set; }
            public int VoteCount { get; set; }
        }
    }
}
