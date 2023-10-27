using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace LaundryMidlePlatform.Pages.TypePage
{
    public class EditModel : PageModel
    {
        private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        public EditModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BusinessObjects.Models.Type Type { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var type =  await _context.Types.FirstOrDefaultAsync(m => m.TypeId == id);
            if (type == null)
            {
                return NotFound();
            }
            Type = type;
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

            _context.Attach(Type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(Type.TypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TypeExists(int id)
        {
          return (_context.Types?.Any(e => e.TypeId == id)).GetValueOrDefault();
        }
    }
}
