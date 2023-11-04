using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Interface;
using Repository.Implements;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageMachine
{
    public class IndexModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public IndexModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}

        private readonly IMachineRepository machineRepository = new MachineRepository();
        private readonly IUserRepository userRepository = new UserRepository();
        private readonly IStoreRepository storeRepository = new StoreRepository();

        public IList<WashingMachine> WashingMachine { get;set; } = default!;

        public IActionResult OnGetAsync()
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
                        WashingMachine = machineRepository.GetWashingMachinesByStoreId(u.UserId);
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
