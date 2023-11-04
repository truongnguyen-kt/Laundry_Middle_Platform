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
using Newtonsoft.Json;

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

        [BindProperty]
        public String Submit { get; set; }

        [BindProperty]
        public OrderInvoice OrderInvoice { get; set; }

        public void OnGet(int OrderId)
        {
            Console.WriteLine("Order Id" + OrderId);
            OrderInvoice = makeOrderDetail.CalculateOrderTimeLine(OrderId);

            Orders = orderRepository.findOrderById(OrderId);
            CustomerName = userRepository.GetUserById((int)Orders.CustomerId).FirstName + " " + userRepository.GetUserById((int)Orders.CustomerId).LastName;
            OrderID = Orders.OrderId.ToString();
            OrderPrice = OrderInvoice.totalPrice.ToString();
            StartTime = OrderInvoice.startDateTime.ToString();
            EndTime = OrderInvoice.finishDateTime.ToString();

            Console.WriteLine(OrderInvoice.ToString());
            // Chuy?n ??i OrderInvoice thành chu?i JSON
            var orderInvoiceJson = JsonConvert.SerializeObject(OrderInvoice);
            ViewData["OrderInvoiceJson"] = orderInvoiceJson;
        }

        public IActionResult OnPost(string submit, string orderInvoice)
        {
            var deserializedOrderInvoice = JsonConvert.DeserializeObject<OrderInvoice>(orderInvoice);
            OrderInvoice Order_Invoice = (OrderInvoice) deserializedOrderInvoice;
            if (submit.Equals("Order"))
            {
                makeOrderDetail.OrderLaundry(Order_Invoice);
            }
            else if (submit.Equals("Cancel"))
            {
                makeOrderDetail.CancelOrder(Order_Invoice.orderId);
            }
            return Page();
        }
    }
}
