using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implements;
using Repository.Interface;
using System.Text.RegularExpressions;
using Validation;

namespace LaundryMidlePlatform.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserRepository _userRepository = new UserRepository();



        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (email == null)
            {
                return Redirect("../Index");
            }

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                User = _userRepository.GetUserById((int)id);
            }


            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

            try
            {
                _userRepository.UpdateUser(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ViewAllUser");
        }

        private bool UserExists(int id)
        {
            return _userRepository.GetUserById(id) != null;
        }
        private bool ValidateInputs()
        {
            Utils utils = new Utils();
            var isValid = true;

            if (string.IsNullOrEmpty(User.Email))
            {
                ModelState.AddModelError("User.Email", "Email cannot null");
                isValid = false;
            }
            else if (Regex.IsMatch(User.Email, @"\s"))
            {
                ModelState.AddModelError("User.Email", "Email cannot contain spaces");
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

            if (string.IsNullOrEmpty(User.Address))
            {
                ModelState.AddModelError("User.Address", "Address cannot null");
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
                else if (!utils.checkTelephoneFormat(User.Phone))
                {
                    ModelState.AddModelError("User.Phone", "Phone must has length from 10 to 13!");
                    isValid = false;
                }
            }

            if (User.DateOfBirth == DateTime.MinValue)
            {
                ModelState.AddModelError("User.DateOfBirth", "DateOfBirth cannot null");
                isValid = false;
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
