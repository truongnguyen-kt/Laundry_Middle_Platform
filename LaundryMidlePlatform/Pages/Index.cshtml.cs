﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implement;
using Repository.Interface;
using Validation;

namespace LaundryMidlePlatform.Pages
{
    public class IndexModel : PageModel
    {
        private Utils validation = new Utils();
        private readonly ILogger<IndexModel> _logger;
        private IUserRepository _user = new UserRepository();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Error { get; set; }




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
                if (user.RoleId == 2) return RedirectToPage("/guest");
                if (user.RoleId == 3) return RedirectToPage("/store");
                return RedirectToPage("/admin");
            }
            else
            {  
                Error = "Invalid Email or Password";
                return Page();
            }

        }

        
    }
}

