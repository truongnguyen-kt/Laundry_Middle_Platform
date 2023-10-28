﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implement;
using Repository.Interface;

namespace LaundryMidlePlatform.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly IUserRepository _userRepository = new UserRepository();



        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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
                _userRepository.DeleteUser(User.UserId);
            }

            return RedirectToPage("./Index");
        }
    }
}
