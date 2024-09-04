using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.Context;
using Data.Models;
using Data.DataAccess;

namespace Event_Organizer.web.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IDataAccess _dataAccess;
		private readonly ILogger<IndexModel> _logger;
		[BindProperty]
        public string EventName { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IDataAccess dataAccess)
		{
			_logger = logger;
			_dataAccess = dataAccess;

		}

		public void OnGet()
		{

		}
		public IActionResult OnPost()
		{
			if (EventName is not null)
			{
				_dataAccess.PostEvent(new Event { Name = EventName });
				return RedirectToPage("/UserSelect");
			}
			else
			{
				return Page(); //to original page
			}
		}
	}
}
