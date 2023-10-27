using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository;
using Repository.Implements;

namespace LaundryMidlePlatform.Pages.TypePage
{
    public class IndexModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public IndexModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}

        TypeRepository repo = new TypeRepository();
        public IList<BusinessObjects.Models.Type> Type { get;set; } = default!;

        public void OnGetAsync()
        {
            //if (_context.Types != null)
            //{
                Type = repo.findAllTypes();
                //Type = await _context.Types.ToListAsync();
            //}
        }
    }
}
