using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Interface;
using Repository.IRepository;
using Repository.Implements;
using System.Collections.ObjectModel;

namespace LaundryMidlePlatform.Pages.Stores
{
    public class IndexModel : PageModel
    {
        private readonly IStoreRepository storeRepository = new StoreRepository();
        private readonly IMachineRepository machineRepository = new MachineRepository();



        public string Email { get; set; }
        public IList<Store> Store { get; set; } = default!;

        public int? RoleId { get; set; }

        [BindProperty]
        public string StoreName { get; set; }
        bool flag = true;
        public IActionResult OnGetAsync()
        {

            
            string email = HttpContext.Session.GetString("customerEmail");
            if (email == null)
            {
                return Redirect("../Index");

            }
            else
            {
                Email = email;
            }
            if(flag)
            {
                Store = storeRepository.GetAllStores();
            }
            
            foreach (var store in Store)
            {
                foreach(var item in store.WashingMachines)
                {
                    if(item.Status == true)
                    {
                        store.WashingMachines.Remove(item);
                    }
                }
            }
            return Page();
        }

        public IActionResult OnPostAsync()
        {
            Store = storeRepository.GetAllStores().Where(x => x.StoreName.ToUpper().Contains(StoreName.ToUpper())).ToList();
            flag = false;
            return OnGetAsync();
        }
    }
}
