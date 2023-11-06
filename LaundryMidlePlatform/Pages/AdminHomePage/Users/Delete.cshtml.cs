﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implements;
using Repository.Interface;

namespace LaundryMidlePlatform.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository _userRepository = new UserRepository();



        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public String Error { get; set; }

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
            User = _userRepository.GetUserById((int)id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = _userRepository.GetUserById((int)id);

            if (User != null)
            {
                var check = _userRepository.DeleteUser(User.UserId);
                if (check == false)
                {
                    Error = "Customer has order is pending or processing, you can't delete !";
                    return Page();
                }
            }

            return RedirectToPage("./ViewAllUser");
        }
    }
}
