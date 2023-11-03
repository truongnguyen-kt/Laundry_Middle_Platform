using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Interface;
using Repository.IRepository;
using Repository.Implements;

namespace LaundryMidlePlatform.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly IStoreRepository storeRepository = new StoreRepository();



        public string Email { get; set; }
        public IList<Store> Store { get; set; } = default!;

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
            Store = storeRepository.GetAllStores();
            return Page();
        }
    }
}
