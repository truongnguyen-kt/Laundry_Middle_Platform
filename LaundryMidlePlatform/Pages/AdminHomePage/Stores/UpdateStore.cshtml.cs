using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implement;
using Repository.Interface;
using Repository.IRepository;
using Repository.Implements;
using System.Text.RegularExpressions;

namespace LaundryMidlePlatform.Pages.Stores
{
    public class EditModel : PageModel
    {
        private readonly IStoreRepository _storeRepository = new StoreRepository();



        [BindProperty]
        public Store Store { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Store = _storeRepository.GetStoreById((int)id);
            }


            if (Store == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ValidateInputs())
            {
                return Page();
            }
            try
            {
                _storeRepository.UpdateStore(Store, Store.StoreId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(Store.StoreId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ViewAllStore");
        }

        private bool StoreExists(int id)
        {
            return _storeRepository.GetStoreById(id) != null;
        }

        private bool ValidateInputs()
        {
            var isValid = true;

            if (string.IsNullOrEmpty(Store.Address))
            {
                ModelState.AddModelError("Store.Address", "Address cannot null");
                isValid = false;
            }
            /*else
            {
                // Regular expression pattern for email validation
                string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

                if (!Regex.IsMatch(User.Email, emailPattern))
                {
                    ModelState.AddModelError("User.Email", "Invalid email address");
                    return false;
                }
            }*/

            if (Store.Price == null)
            {
                ModelState.AddModelError("Store.Price", "Price cannot null");
                isValid = false;
            }
            if (string.IsNullOrEmpty(Store.StoreName))
            {
                ModelState.AddModelError("(Store.StoreName.", "StoreName cannot null");
                isValid = false;
            }

            if (Store.Status == null)
            {
                ModelState.AddModelError("Store.Status", "Status cannot null");
                isValid = false;
            }
            if (string.IsNullOrEmpty(Store.Phone))
            {
                ModelState.AddModelError("Store.Phone", "Phone cannot null");
                isValid = false;
            }
            else
            {
                // Kiểm tra xem giá trị Phone chỉ chứa ký tự số từ 0 đến 9
                if (!Regex.IsMatch(Store.Phone, "^[0-9]+$"))
                {
                    ModelState.AddModelError("Store.Phone", "Phone must contain only numeric digits (0-9)");
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}
