using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Implements;
using Repository.Interface;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageMachine
{
    public class DetailsModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public DetailsModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}
        private readonly IMachineRepository machineRepository = new MachineRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IStoreRepository storeRepository = new StoreRepository();
        [BindProperty]
        public WashingMachine WashingMachine { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (email == null)
            {
                return Redirect("../Index");
            }
            else
            {
                User u = userRepository.findUserByEmail(email);
                if (u != null)
                {
                    if (u.RoleId != 3)
                    {
                        return Redirect("../Index");
                    }
                    else
                    {
                        if (id == null)
                        {
                            return NotFound();
                        }

                        //var washingmachine = await _context.WashingMachines.FirstOrDefaultAsync(m => m.MachineId == id);
                        var washingmachine = machineRepository.GetWashingMachineById((int)id);
                        washingmachine.Store = storeRepository.GetStoreById((int)washingmachine.StoreId);
                        if (washingmachine == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            WashingMachine = washingmachine;
                        }
                        return Page();
                    }
                }
                else
                {
                    return Redirect("../Index");
                }
            }
        }
    }
}
