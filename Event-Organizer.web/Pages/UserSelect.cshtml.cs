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
            var eventId = EventId;
            //Users = _dataAccess.GetEventUsers(1);
        }
		public void OnPost()
		{
			//post new user, make sure it appears on the list (refresh)
		}
	}
}
