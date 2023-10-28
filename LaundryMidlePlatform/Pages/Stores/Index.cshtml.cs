﻿using System;
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

namespace LaundryMidlePlatform.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly IStoreRepository storeRepository = new StoreRepository();




        public IList<Store> Store { get; set; } = default!;

        public int? RoleId { get; set; }
        public IActionResult OnGetAsync()
        {

            Store = storeRepository.GetAllStores();
            return Page();
        }
    }
}
