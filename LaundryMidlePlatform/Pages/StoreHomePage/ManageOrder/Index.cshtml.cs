using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageOrder
{
    public class IndexModel : PageModel
    {
        private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        public IndexModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Store).ToListAsync();
            }
        }
    }
}
