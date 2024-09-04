using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{
    public class EventConfirmationModel : PageModel
    {
        public string? EventLink { get; set; }
        public Guid? EventId { get; set; }

        public void OnGet()
        {
            // Retrieve TempData values
            EventLink = TempData["EventLink"] as string;
            EventId = TempData["EventId"] as Guid?;
        }
    }
}
