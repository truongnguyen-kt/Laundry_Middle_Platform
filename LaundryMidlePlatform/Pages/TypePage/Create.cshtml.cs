using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;

namespace LaundryMidlePlatform.Pages.TypePage
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        public CreateModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BusinessObjects.Models.Type Type { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Types == null || Type == null)
            {
                return Page();
            }

            _context.Types.Add(Type);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
