using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.IRepository;
using Repository.Implements;
using Repository.Interface;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Implements;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Validation;

namespace LaundryMidlePlatform.Pages.CustomerHomePage
{
    public class updateProfileCustomerModel : PageModel
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

        public string Error { get; set; }
        public string Success { get; set; }
        public string TypeGender { get; set; }

        public IActionResult OnPostAsync()
        {
            if (validation.CheckContainDigit(User.FirstName))
            {
                Error = "Customer First Name can not contains digits. Can not update Customer Account";
                return Page();
            }
            if (validation.CheckContainDigit(User.LastName))
            {
                Error = "Customer Last Name can not contains digits. Can not update Customer Account";
                return Page();
            }

            if (!validation.IsValidDOB(User.DateOfBirth))
            {
                Error = "Customer Birthday Year have between 1900 and 2023 and not Greater Than Current Date. Can not update Customer Account";
                return Page();
            }

            if (!validation.checkTelephoneFormat(User.Phone))
            {
                Error = "Phone Number cannnot consist letters and Length have to be between 10 - 13 numbers. Can not update Customer Account";
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                userRepository.UpdateUser(User);
                Success = "Update Customer Successfully";

            }
            catch (Exception ex)
            {
                Error = "Fail to Update Customer Account";
            }
            return Page();
        }

    }
}
