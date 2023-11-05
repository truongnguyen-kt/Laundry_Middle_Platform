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
using Validation;

namespace LaundryMidlePlatform.Pages.CustomerHomePage
{
    public class ViewAllStoresModel : PageModel
    {
        private IUserRepository userRepository = new UserRepository();

        private Utils validation = new Utils();
        public string Email { get; private set; }

        [BindProperty]
        public User User { get; set; } = default!;

        private IStoreRepository storeRepository = new StoreRepository();
        public IList<Store> Store { get; set; } = default!;
        [BindProperty]
        public string StoreName { get; set; }
        bool flag = true;

        public IActionResult OnGetAsync()
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                User = userRepository.findUserByEmail(Email);
            }
            if (flag)
            {
                Store = storeRepository.GetAllStores();
            }
            return Page();
        }

        [BindProperty]
        public string SelectedStoreId { get; set; } = default!;

        public IActionResult OnPostAsync()
        {
            Store = storeRepository.GetAllStores().Where(x => x.StoreName.ToUpper().Contains(StoreName.ToUpper())).ToList();
            flag = false;
            return OnGetAsync();
        }
    }
}
