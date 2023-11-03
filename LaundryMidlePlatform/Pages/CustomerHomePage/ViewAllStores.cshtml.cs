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
using Repository.Implement;
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

        public void OnGet()
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                User = userRepository.findUserByEmail(Email);
            }
            Store = storeRepository.GetAllStores();
        }

        [BindProperty]
        public string SelectedStoreId { get; set; } = default!;

        //public IActionResult OnPost(int storeId) // Add storeId as a parameter
        //{
        //    // Use storeId here
        //    // You can pass it to the next page if needed
        //    //return RedirectToPage("/CustomerHomePage/OrderPage/CustomerMakeOrderDetail", new { storeId = SelectedStoreId });
        //}
    }
}
