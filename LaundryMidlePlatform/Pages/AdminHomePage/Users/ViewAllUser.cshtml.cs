using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implement;
using Repository.Interface;
using Microsoft.AspNetCore.SignalR;

namespace LaundryMidlePlatform.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository UserRepository = new UserRepository();



        public string Email { get; private set; }
        public IList<User> User { get; set; } = default!;

        public int? RoleId { get; set; }
        public IActionResult OnGetAsync()
        {   
            string email = HttpContext.Session.GetString("customerEmail");
            if (email == null)
            {
                return Redirect("../Index");
                
            }
            else
            {
                Email = email;
            }

            User = UserRepository.GetAllUsers();
            return Page();
        }
    }
}
