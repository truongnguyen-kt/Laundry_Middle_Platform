using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.Interface;
using Repository;
using Repository.Implements;

namespace LaundryMidlePlatform.Pages.Orders
{
    public class IndexModel : PageModel
    {
        //private readonly BusinessObjects.Models.LaundryMiddlePlatformContext _context;

        //public IndexModel(BusinessObjects.Models.LaundryMiddlePlatformContext context)
        //{
        //    _context = context;
        //}

        public IList<Order> Order { get;set; } = default!;
        private IOrderRepository orderRepository = new OrderRepository();

        public void OnGetAsync()
        {
            Order = orderRepository.findAllOrders();
            if(Order != null)
            {
                foreach (var order in Order)
                {
                }
            }
        }
    }
}
