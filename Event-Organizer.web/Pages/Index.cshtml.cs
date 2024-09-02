using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		[BindProperty]
        public string EventName { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public void OnGet()
		{

		}
		public IActionResult OnPost()
		{
			if (EventName is not null)
			{
				return RedirectToPage("/UserSelect");
			}
			else
			{
				return Page(); //to original page
			}
		}
	}
}
