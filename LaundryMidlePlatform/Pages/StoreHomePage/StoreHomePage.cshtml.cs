using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.Models;
using Repository.IRepository;
using Repository;

namespace LaundryMidlePlatform.Pages.StoreHomePage
{
    public class StoreHomePage : PageModel
    {
        private IOrderRepository orderRepository = new OrderRepository();

        public IList<Order> Orders { get; set; } = default!;

        public void OnGetAsync()
        {
            //if (_context.Stores != null)
            //{
            //    Store = await _context.Stores.ToListAsync();
            //}
            Orders = (IList<Order>)orderRepository.findAllOrders();
        }
    }
}
