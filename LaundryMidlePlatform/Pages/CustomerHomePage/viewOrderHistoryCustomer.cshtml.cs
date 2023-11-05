using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Implements;
using Repository.Interface;
using Validation;

namespace LaundryMidlePlatform.Pages.CustomerHomePage
{
    public class viewOrderHistoryCustomerModel : PageModel
    {
        private IUserRepository userRepository = new UserRepository();
        private Utils validation = new Utils();
        private readonly IStoreRepository storeRepository = new StoreRepository();
        private readonly IOrderRepository orderRepository = new OrderRepository();
        [BindProperty]
        public IList<Order> Order { get; set; } = default!;
        [BindProperty]
        public string Email { get; private set; }

        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public DateTime StartDateTime { get; set; }

        [BindProperty]
        public DateTime EndDateTime { get; set; }
        [BindProperty]
        public string Error { get; set; }

        [BindProperty]
        public string Success { get; set; }
        public void OnGet()
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                Console.WriteLine("Email Customer: " + Email);
                User = userRepository.findUserByEmail(Email);
                Order = orderRepository.findAllOrderByCustomerId(User.UserId);
            }
        }

        public IActionResult OnPost()
        {
            User user = null;
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                user = userRepository.findUserByEmail(email);
            }
            Console.WriteLine("Email Customer: " + Email);

            if (string.IsNullOrEmpty(StartDateTime.ToString()))
            {
                Error = "Start Date Time Can not be Null. Can not Filter Order";
                return Page();
            }
            if (string.IsNullOrEmpty(EndDateTime.ToString()))
            {
                Error = "End Date Time Can not be Null. Can not Filter Order";
                return Page();
            }
            if (!validation.IsValidDOB(StartDateTime))
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
            IList<Order> orders = orderRepository.findAllOrderBetweenStartDateTimeAndStartEndTimeAndCustomerId(startDateTime, endDateTime, user.UserId);
            foreach (Order order in orders)
            {
                Console.WriteLine("Order: " + order.ToString());
            }
            Order = orders;

            return Page();
        }
    }
}
