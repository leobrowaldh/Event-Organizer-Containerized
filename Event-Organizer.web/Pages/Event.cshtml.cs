using Data.DataAccess;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{
    public class EventModel(IDataAccess injectedDataAccess) : PageModel
    {
        private readonly IDataAccess _dataAccess = injectedDataAccess;

		public ICollection<Activity> Activities { get; set; }
        public Event? ActiveEvent { get; set; }
        public User CurrentUser { get; set; }
        public void OnGet()
        {
            //ActiveEvent = _dataAccess.GetEvent(1);
            //Activities = _dataAccess.GetEventActivities(1);
            //CurrentUser = _dataAccess.GetUser(1);
        }
    }
}
