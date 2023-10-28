using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.IRepository;
using Repository.Implements;
using Repository.Interface;
using Microsoft.EntityFrameworkCore.Migrations;


namespace LaundryMidlePlatform.Pages.CustomerHomePage
{
    public class CustomerHomePageModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public IndexModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}

        private IStoreRepository storeRepository = new StoreRepository();

        public IList<Store> Store { get; set; } = default!;

        public void OnGetAsync()
        {
            //if (_context.Stores != null)
            //{
            //    Store = await _context.Stores.ToListAsync();
            //}
            Store = (IList<Store>)storeRepository.GetAllStores();
        }
    }
}
