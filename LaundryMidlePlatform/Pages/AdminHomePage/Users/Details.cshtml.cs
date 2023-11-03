using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implements;

namespace LaundryMidlePlatform.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly UserRepository userRepository = new UserRepository();

        public User User { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = userRepository.GetUserById((int)id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
