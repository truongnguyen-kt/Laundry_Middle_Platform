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


namespace LaundryMidlePlatform.Pages.CustomerHomePage
{
    public class CustomerHomePageModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public IndexModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}

        public string Email { get; private set; }
        public void OnGetAsync()
        {
            //if (_context.Stores != null)
            //{
            //    Store = await _context.Stores.ToListAsync();
            //}
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
            }
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Remove("customerEmail");
            return RedirectToPage("/Index");
        }
    }
}
