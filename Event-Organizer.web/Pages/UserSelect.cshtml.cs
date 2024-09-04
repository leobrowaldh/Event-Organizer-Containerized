using Event_Organizer.web.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{

    public class UserSelectModel(IDataAccess injectedDataAccess) : PageModel
    {
		private readonly IDataAccess _dataAccess = injectedDataAccess;
		public ICollection<User>? Users { get; set; }
        [BindProperty]
        public string? NewUser { get; set; }
        public void OnGet()
        {
			Users = _dataAccess.GetEventUsers(0);
        }
		public IActionResult OnPost()
		{
			if (NewUser is not null)
			{
				return RedirectToPage("/Event");
			}
			else
			{
				return Page(); //to original page
			}
		}
	}
}
