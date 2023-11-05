using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Interface;
using Repository.Implements;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Validation;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageMachine
{
    public class EditModel : PageModel
    {
        private readonly IMachineRepository machineRepository = new MachineRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IStoreRepository storeRepository = new StoreRepository();
        private Utils validation = new Utils();
        [BindProperty]
        public WashingMachine WashingMachine { get; set; } = default!;

        [BindProperty]
        public string Error { get; set; }

        [BindProperty]
        public string Success { get; set; }


        public IActionResult OnGetAsync(int? id)
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

                        var washingmachine = machineRepository.GetWashingMachineById((int)id);
                        if (washingmachine == null)
                        {
                            return NotFound();
                        }
                        WashingMachine = washingmachine;
                        Store s = storeRepository.GetStoreById((int)WashingMachine.StoreId);
                        
                        //ViewData["StoreId"] = new SelectList(s, "StoreName");
                        return Page();
                    }
                }
                else
                {
                    return Redirect("../Index");
                }
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPostAsync()
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
                        if (string.IsNullOrEmpty(WashingMachine.MachineName))
                        {
                            Error = "Machine Name can not be null or empty. Can not update Washing Machine";
                            return Page();
                        }
                        if (string.IsNullOrEmpty(WashingMachine.Performmance.ToString()))
                        {
                            Error = "Machine Performance can not be null or empty. Can not update Washing Machine";
                            return Page();
                        }
                        if (validation.CheckContainLetter(WashingMachine.Performmance.ToString()))
                        {
                            Error = "Machine Performance only can contain digits. Can not update Washing Machine";
                            return Page();
                        }
                        if (string.IsNullOrEmpty(WashingMachine.Status.ToString()))
                        {
                            Error = "Machine Status can not be null or empty. Can not update Washing Machine";
                            return Page();
                        }

                        try
                        {
                            machineRepository.UpdateWashingMachine(WashingMachine, WashingMachine.MachineId);
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!WashingMachineExists(WashingMachine.MachineId))
                            {
                                return NotFound();
                            }
                            else
                            {
                                throw;
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

        private bool WashingMachineExists(int id)
        {
          return machineRepository.GetWashingMachineById(id) != null;
        }
    }
}
