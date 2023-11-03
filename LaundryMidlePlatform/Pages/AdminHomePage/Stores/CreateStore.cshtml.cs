using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DataAccess;
using Repository.Implements;
using Repository.IRepository;
using System.Text.RegularExpressions;
using Validation;

namespace LaundryMidlePlatform.Pages.Stores
{
    public class CreateModel : PageModel
    {
        private readonly LaundryMiddlePlatformContext _context = new LaundryMiddlePlatformContext();

        private readonly StoreRepository storeRepository = new StoreRepository();

        private Utils validation = new Utils();

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Store Store { get; set; } = default!;

        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Stores == null || Store == null)
            {
                return Page();
            }
            if (!ValidateInputs())
            {
                return Page();
            }

            storeRepository.AddStore(Store);

            return RedirectToPage("./ViewAllStore");
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
            }else if (!validation.checkTelephoneFormat(Store.Phone))
            {
                ModelState.AddModelError("Store.Phone", "Phone Number cannnot consist letters and Length have to be between 10 - 13 numbers");
                isValid = false;
            } else
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
