using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implement;
using Repository.Implements;

namespace LaundryMidlePlatform.Pages.Stores
{
    public class DetailsModel : PageModel
    {
        private readonly StoreRepository storeRepository = new StoreRepository();

        public Store Store { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Store = storeRepository.GetStoreById((int)id);

            if (Store == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
