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
using Repository.IRepository;
using Repository.Implements;
using DataAccess;

namespace LaundryMidlePlatform.Pages.Stores
{
    public class DeleteModel : PageModel
    {
        private readonly IStoreRepository _storeRepository = new StoreRepository();



        [BindProperty]
        public Store Store { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Store = _storeRepository.GetStoreById((int)id);

            if (Store == null)
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

            Store = _storeRepository.GetStoreById((int)id);

            if (Store != null)
            {
                _storeRepository.DeleteStore(Store.StoreId);
            }

            return RedirectToPage("./ViewAllStore");
        }
    }
}
