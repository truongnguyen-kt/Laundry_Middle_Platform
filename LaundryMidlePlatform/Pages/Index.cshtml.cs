using LaundryMidlePlatform.Pages.SessionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implements;
using Repository.Interface;
using Validation;

namespace LaundryMidlePlatform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private Utils validation = new Utils();
        private IUserRepository _user = new UserRepository();

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Error { get; set; }

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!validation.IsNotEmptyString(Email))
            {
                Error = "Email is not empty!";
                return Page();
            }
            if (!validation.IsEmail(Email))
            {
                Error = "Sai email, example: a@gmail.com";
                return Page();
            }
            var user = _user.GetCustomerByEmailAndPassword(Email.Trim(), Password);

            if (user != null)
            {
                HttpContext.Session.SetString("customerEmail", Email.Trim());
                if (user.RoleId == 2)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "customer", user);
                    return RedirectToPage("/CustomerHomePage/CustomerHomePage");
                }
                if (user.RoleId == 3)
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "store", user);
                    return RedirectToPage("/StoreHomePage/ManageMachine/Index");
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "admin", user);
                return RedirectToPage("/AdminHomePage/Users/ViewAllUser");
            }
            else
            {
                Error = "Invalid Email or Password";
                return Page();
            }
        }
    }
}