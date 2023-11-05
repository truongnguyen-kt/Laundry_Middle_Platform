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
using Validation;

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
        private Utils validation = new Utils();


        [BindProperty]
        public WashingMachine WashingMachine { get; set; } = default!;
        [BindProperty]
        public Store Store { get; set; } = default;

        [BindProperty]
        public string Error {  get; set; }

        [BindProperty]
        public string Success { get; set; }

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
                        if(string.IsNullOrEmpty(WashingMachine.MachineName))
                        {
                            Error = "Machine Name can not be null or empty. Can not create Washing Machine";
                            return Page();
                        }
                        if(string.IsNullOrEmpty(WashingMachine.Performmance.ToString()))
                        {
                            Error = "Machine Performance can not be null or empty. Can not create Washing Machine";
                            return Page();
                        }
                        if(validation.CheckContainLetter(WashingMachine.Performmance.ToString()))
                        {
                            Error = "Machine Performance only can contain digits. Can not create Washing Machine";
                            return Page();
                        }
                        if (string.IsNullOrEmpty(WashingMachine.Status.ToString()))
                        {
                            Error = "Machine Status can not be null or empty. Can not create Washing Machine";
                            return Page();
                        }

                        WashingMachine.StoreId = Store.StoreId;
                        WashingMachine newWashingMachine = new WashingMachine(WashingMachine.MachineName, WashingMachine.Performmance, WashingMachine.Status, WashingMachine.StoreId);
                        machineRepository.AddWashingMachine(newWashingMachine);
                        
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
