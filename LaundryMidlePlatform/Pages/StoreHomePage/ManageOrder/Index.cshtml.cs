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
using Validation;

namespace LaundryMidlePlatform.Pages.StoreHomePage.ManageOrder
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
        private readonly IOrderRepository orderRepository = new OrderRepository();
        private Utils validation = new Utils();

        [BindProperty]
        public IList<Order> Order { get;set; } = default!;

        [BindProperty]
        public DateTime StartDateTime { get; set; }

        [BindProperty]
        public DateTime EndDateTime { get; set; }

        [BindProperty]
        public string Error { get; set; }

        [BindProperty]
        public string Success { get; set; }

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
                        Order = orderRepository.findAllOrderByStoreId(u.UserId);
                        return Page();
                    }
                }
                else
                {
                    return Redirect("../Index");
                }
            }
        }

        public IActionResult OnPostAsync()
        {
            if(string.IsNullOrEmpty(StartDateTime.ToString())) 
            {
                Error = "Start Date Time Can not be Null. Can not Filter Order";
                return Page();
            }
            if (string.IsNullOrEmpty(EndDateTime.ToString()))
            {
                Error = "End Date Time Can not be Null. Can not Filter Order";
                return Page();
            }
            if(!validation.IsValidDOB(StartDateTime))
            {
                Error = "Start Date Time is invalid DateTime. Can not Filter Order";
                return Page();
            }
            if (!validation.IsValidDOB(EndDateTime))
            {
                Error = "End Date Time is invalid DateTime. Can not Filter Order";
                return Page();
            }
            DateTime startDateTime = validation.convertDateTime(StartDateTime);
            DateTime endDateTime = validation.convertDateTime(EndDateTime);
            //Console.WriteLine("StartDateTime: " + startDateTime + " EndDateTime: " + endDateTime);
            IList<Order> orders = orderRepository.findAllOrderBetweenStartDateTimeAndStartEndTime(startDateTime, endDateTime);
            foreach(Order order in orders)
            {
                Console.WriteLine("Order: " + order.ToString());
            }
            Order = orders;

            return Page();
        }
    }
}
