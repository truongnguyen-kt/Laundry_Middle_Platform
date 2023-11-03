using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implements;
using Repository.Interface;
using Validation;

namespace LaundryMidlePlatform.Pages.CustomerHomePage
{
    public class viewOrderHistoryCustomerModel : PageModel
    {
        private IUserRepository userRepository = new UserRepository();
        private Utils validation = new Utils();
        public string Email { get; private set; }

        [BindProperty]
        public User User { get; set; } = default!;
        public void OnGet()
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                User = userRepository.findUserByEmail(Email);
            }
        }
    }
}
