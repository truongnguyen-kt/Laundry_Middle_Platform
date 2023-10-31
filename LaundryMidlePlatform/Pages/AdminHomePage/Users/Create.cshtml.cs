using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Repository.Implement;
using Repository.Interface;
using System.Text.RegularExpressions;

namespace LaundryMidlePlatform.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserRepository _userRepository = new UserRepository();


        public IActionResult OnGet()
        {
            return Page();
        }


        [BindProperty]
        public User User { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ValidateInputs())
            {
                return Page();
            }
            var UserByEmail = _userRepository.GetUserByEmail(User.Email);
            bool flag = false;
            if (UserByEmail != null && UserByEmail.Count > 0)
            {
                foreach (var obj in UserByEmail)
                {
                    if (obj.Email.CompareTo(User.Email) == 0)
                    {
                        if (obj.UserId != User.UserId)
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (flag)
            {
                ModelState.AddModelError("User.Email", "Email Already Owned");
                return Page();
            }
            _userRepository.AddNewCustomer(User);

            return RedirectToPage("./ViewAllUser");
        }
        private bool ValidateInputs()
        {
            var isValid = true;

            if (string.IsNullOrEmpty(User.Email))
            {
                ModelState.AddModelError("User.Email", "Email cannot null");
                isValid = false;
            }
            else
            {
                // Regular expression pattern for email validation
                string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

                if (!Regex.IsMatch(User.Email, emailPattern))
                {
                    ModelState.AddModelError("User.Email", "Invalid email address");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(User.FirstName))
            {
                ModelState.AddModelError("User.FirstName", "FirstName cannot null");
                isValid = false;
            }
            if (string.IsNullOrEmpty(User.LastName))
            {
                ModelState.AddModelError("(User.LastName.", "LastName cannot null");
                isValid = false;
            }
            if (string.IsNullOrEmpty(User.Phone))
            {
                ModelState.AddModelError("(User.Phone.", "Phone cannot null");
                isValid = false;
            }
            else
            {
                // Kiểm tra xem giá trị Phone chỉ chứa ký tự số từ 0 đến 9
                if (!Regex.IsMatch(User.Phone, "^[0-9]+$"))
                {
                    ModelState.AddModelError("User.Phone", "Phone must contain only numeric digits (0-9)");
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(User.Address))
            {
                ModelState.AddModelError("User.Address", "Address cannot null");
                isValid = false;
            }

            if (User.DateOfBirth == DateTime.MinValue)
            {
                ModelState.AddModelError("User.DateOfBirth", "DateOfBirth cannot null");
                isValid = false;
            }
            else
            {
                // Tính khoảng thời gian giữa ngày sinh và ngày hiện tại
                TimeSpan ageDifference = DateTime.Now - (DateTime)User.DateOfBirth;

                // Tính tuổi dựa trên khoảng thời gian
                int age = (int)(ageDifference.TotalDays / 365.25); // Sử dụng 365.25 để xem xét năm nhuận

                // Kiểm tra xem tuổi có lớn hơn 11 hay không
                if (age < 12)
                {
                    ModelState.AddModelError("User.DateOfBirth", "You must be at least 12 years old.");
                    isValid = false;
                }
            }
            if (string.IsNullOrEmpty(User.Password))
            {
                ModelState.AddModelError("User.Password", "Password cannot null");
                isValid = false;
            }

            return isValid;
        }
    }
}
