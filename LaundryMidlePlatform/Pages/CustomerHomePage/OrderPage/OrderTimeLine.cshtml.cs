using Validation;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Implements;
using BusinessObjects;

namespace LaundryMidlePlatform.Pages.CustomerHomePage.OrderPage
{
    public class OrderTimeLineModel : PageModel
    {
        private IUserRepository userRepository = new UserRepository();
        private Utils validation = new Utils();
        private IStoreRepository storeRepository = new StoreRepository();
        private ITypeRepository typeRepository = new TypeRepository();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private IOrderRepository orderRepository = new OrderRepository();
        private IMakeOrderDetail makeOrderDetail = new MakeOrderDetail();

        [BindProperty]
        public Order Orders { get; set; }

        [BindProperty]
        public String CustomerName { get; set; }

        [BindProperty]
        public String OrderID { get; set; }

        [BindProperty]
        public String OrderPrice { get; set; }

        [BindProperty]
        public String StartTime { get; set; }

        [BindProperty]
        public String EndTime { get; set; }


        public void OnGet(int OrderId)
        {
            Console.WriteLine("Order Id" + OrderId);
            OrderInvoice orderInvoice = makeOrderDetail.CalculateOrderTimeLine(OrderId);
            Orders = orderRepository.findOrderById(OrderId);
            CustomerName = userRepository.GetUserById((int)Orders.CustomerId).FirstName + " " + userRepository.GetUserById((int)Orders.CustomerId).LastName;
            OrderID = Orders.OrderId.ToString();
            OrderPrice = orderInvoice.totalPrice.ToString();
            StartTime = orderInvoice.startDateTime.ToString();
            EndTime = orderInvoice.finishDateTime.ToString();

        }
    }
}
