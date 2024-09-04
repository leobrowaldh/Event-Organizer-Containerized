using Event_Organizer.web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{
    public class EventModel(IDataAccess injectedDataAccess) : PageModel
    {
        private readonly IDataAccess _dataAccess = injectedDataAccess;

		public ICollection<Activity> Activities { get; set; }
        public Event ActiveEvent { get; set; }
        public User CurrentUser { get; set; }
        public void OnGet()
        {
            Activities = _dataAccess.GetEventActivities(0);
            ActiveEvent = _dataAccess.GetEvent(0);
            CurrentUser = _dataAccess.GetUser(0);
        }
    }
}
