using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Event_Organizer.web.Pages
{
    public class EventConfirmationModel : PageModel
    {
        public bool FirstTimeCreatingEvent { get; set; }
        public string? EventLink { get; set; }
        public Guid? EventId { get; set; }

        public void OnGet()
        {
            // Retrieve TempData values
            EventLink = TempData["EventLink"] as string;
            EventId = TempData["EventId"] as Guid?;

            // Check if the "FirstTimeCreatingEvent" cookie exists
            var cookie = Request.Cookies["FirstTimeCreatingEvent"];

            if (string.IsNullOrEmpty(cookie))
            {
                // If the cookie doesn't exist, it's the user's first visit
                FirstTimeCreatingEvent = true;

                // Set the "FirstVisit" cookie with a value and expiration date
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1) // Set the cookie to expire in 1 year
                };
                Response.Cookies.Append("FirstTimeCreatingEvent", "true", cookieOptions);
            }
            else
            {
                // If the cookie exists, it's not the first visit
                FirstTimeCreatingEvent = false;
            }
        }
    }
}
