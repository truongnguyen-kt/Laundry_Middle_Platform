using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Repository.IRepository;
using Repository;
using Repository.Implements;
using Microsoft.EntityFrameworkCore;

namespace LaundryMidlePlatform.Pages.StoreHomePage
{
    public class CreateMachine : PageModel
    {
        private IMachineRepository machineRepository = new MachineRepository();
        private IStoreRepository storeRepository = new StoreRepository();
        public SelectList StoreIdList { get; set; }
        //public IList<WashingMachine> Machines { get; set; } = default!;

        public IActionResult OnGetAsync()
        {
            //Machines = (IList<WashingMachine>)machineRepository.GetWashingMachines();
            ViewData["StoreId"] = new SelectList(storeRepository.GetAllStoreID());
            return Page();
        }
        [BindProperty]
        public WashingMachine Machine { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Machine == null)
            {
                StoreIdList = new SelectList(storeRepository.GetAllStoreID());
                return Page();
            }
            machineRepository.AddWashingMachine(Machine);
            return Page();
        }
    }
}
