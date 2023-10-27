using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace LaundryMidlePlatform.Pages.TypePage
{
    public class DetailsModel : PageModel
    {
        private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        public DetailsModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        {
            _context = context;
        }

      public BusinessObjects.Models.Type Type { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Types == null)
            {
                return NotFound();
            }

            var type = await _context.Types.FirstOrDefaultAsync(m => m.TypeId == id);
            if (type == null)
            {
                return NotFound();
            }
            else 
            {
                Type = type;
            }
            return Page();
        }
    }
}
