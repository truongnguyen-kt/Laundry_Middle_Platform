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


            return RedirectToPage("/Index");
        }
    }
}
