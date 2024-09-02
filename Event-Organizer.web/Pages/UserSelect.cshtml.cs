using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{

    public class User //mock
    {
        public string Name { get; set; }
    }

    public class UserSelectModel : PageModel
    {
        public IList<User>? Users { get; set; }
        [BindProperty]
        public string? NewUser { get; set; }
        public void OnGet()
        {
			Users = [new User() { Name = "Ahmed"}, new User() { Name = "Olle" }, new User() { Name = "Pepe" },
			new User() { Name = "Tobias"}, new User() { Name = "Yasmine"}, new User() { Name = "Martin"}];
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
