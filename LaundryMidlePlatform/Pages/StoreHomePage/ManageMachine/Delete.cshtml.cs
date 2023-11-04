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
using System.Security.Cryptography.X509Certificates;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageMachine
{
    public class DeleteModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public DeleteModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}

        private readonly IMachineRepository machineRepository = new MachineRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IStoreRepository storeRepository = new StoreRepository();

        [BindProperty]
        public WashingMachine WashingMachine { get; set; } = default!;
        public String StoreName { get; set; }

        [BindProperty]
        public String Error {  get; set; }

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

                        if (washingmachine == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            WashingMachine = washingmachine;
                            StoreName = storeRepository.GetStoreById((int)WashingMachine.StoreId).StoreName;
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

        public async Task<IActionResult> OnPostAsync(int? id)
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
                        //var washingmachine = await _context.WashingMachines.FindAsync(id);
                        var washingmachine = machineRepository.GetWashingMachineById((int)id);

                        if (washingmachine != null)
                        {
                            WashingMachine = washingmachine;
                            StoreName = storeRepository.GetStoreById((int)WashingMachine.StoreId).StoreName;
                            //_context.WashingMachines.Remove(WashingMachine);
                            //await _context.SaveChangesAsync();
                            if (WashingMachine.Status == false) // Running  
                            {
                                Error = "Fail to Delete Washing Machine is state Running";
                                return Page();
                            }
                            else// Ready
                            {
                                machineRepository.DeleteWashingMachine(WashingMachine.MachineId);
                            }
                        }

                        return RedirectToPage("./Index");
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
