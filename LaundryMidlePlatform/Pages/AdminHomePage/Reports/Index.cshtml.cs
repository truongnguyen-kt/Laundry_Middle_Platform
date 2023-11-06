using BusinessObjects.Models;
using LaundryMidlePlatform.Pages.SessionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implements;
using Repository.Interface;

namespace LaundryMidlePlatform.Pages.AdminHomePage.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orderRepository = new OrderRepository();

        public IList<Order> Orders { get; set; }
        [BindProperty] public string? SearchValue { get; set; }

        [BindProperty] public DateTime startDay { get; set; }
        [BindProperty] public DateTime endDay { get; set; }
        [BindProperty] public double? Total { get; set; }
        public IActionResult OnGet()
        {


            Orders = _orderRepository.findAllOrders().Where(o => !o.OrderStatus.Contains("CANCEL")).ToList();
            HttpContext.Session.SetObjectAsJson("cart", null);
            // foreach (var obj in Order)
            // {
            //     if (obj.CustomerId != null)
            //     {
            //         obj.Customer = _customerRepository.GetCustomerById((int)obj.CustomerId);
            //     }
            // }

            return Page();
        }
        public IActionResult OnPostSearch()
        {
            Orders = _orderRepository.findAllOrders();
            if (!ModelState.IsValid)
            {
                // Handle invalid model state
                return Page();
            }

            DateTime currentDay = DateTime.Now.Date;

            if (startDay > currentDay)
            {
                ModelState.AddModelError("startDay", "Start day must not be greater than the current day.");
                return Page();
            }

            if (endDay < startDay)
            {
                ModelState.AddModelError("endDay", "End day must be greater than start day.");
                return Page();
            }

            Orders = _orderRepository.findAllOrders()
                .Where(O => O.FinishDateTime >= startDay
                            && O.FinishDateTime <= endDay
                            && !O.OrderStatus.Contains("CANCEL"))
                .ToList();
            



            Total = Orders.Sum(o => o.TotalPrice);
            var orderByDescending = Orders.OrderByDescending(O => O.TotalPrice).ToList();
            Orders = orderByDescending;
            return Page();
        }
    }
}
