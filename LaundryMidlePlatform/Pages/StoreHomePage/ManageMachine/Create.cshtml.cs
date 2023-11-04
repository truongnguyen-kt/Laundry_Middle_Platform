using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using Repository.Implements;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageMachine
{
    public class CreateModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public CreateModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}
        private readonly IMachineRepository machineRepository = new MachineRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IStoreRepository storeRepository = new StoreRepository();


        [BindProperty]
        public WashingMachine WashingMachine { get; set; } = default!;
        [BindProperty]
        public Store Store { get; set; } = default;
        public IActionResult OnGet()
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
                        //List<String> s = new List<String>();
                        //s.Add(storeRepository.GetStoreById(u.UserId).StoreName);
                        //ViewData["StoreId"] = new SelectList(s, "StoreName");
                        //WashingMachine.StoreId = u.UserId;
                        Store = storeRepository.GetStoreById(u.UserId);
                        return Page();
                    }
                }
                else
                {
                    return Redirect("../Index");
                }
            }
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
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
                        WashingMachine.StoreId = Store.StoreId;
                        WashingMachine.Store = storeRepository.GetStoreById((int)WashingMachine.StoreId);
                        if (!ModelState.IsValid || WashingMachine == null)
                        {
                            return Page();
                        }
                        machineRepository.AddWashingMachine(WashingMachine);
                        
                        //_context.WashingMachines.Add(WashingMachine);
                        //await _context.SaveChangesAsync();

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
