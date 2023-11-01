using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LaundryMidlePlatform.Pages
{
    public class LogoutModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {
            // Clear the user session
            HttpContext.Session.Clear();

            // Redirect to the login page or any other desired page
            return RedirectToPage("/Index");
        }
    }
}
