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
				Event newEvent = new Event { Name = EventName, Id = Guid.NewGuid() };
				_dataAccess.PostEvent(newEvent);

				// Store event info in TempData to pass to the confirmation page
				TempData["EventId"] = newEvent.Id;

                // Build the full URL including scheme, host, and path
                var request = HttpContext.Request;
                string scheme = request.Scheme; // "http" or "https"
                string host = request.Host.Value; // "localhost:5000" or "www.yoursite.com"
                string path = Url.Page("/Event", new { eventId = newEvent.Id }); // Build URL path

                // Combine to create the full URL
                string FullEventLink = $"{scheme}://{host}{path}";

                // Store the full URL in TempData or use it directly
                TempData["EventLink"] = FullEventLink;

                // Redirect to the confirmation page
                return RedirectToPage("/EventConfirmation");
			}
			else
			{
				return Page(); //to original page
			}
		}

	}
}
