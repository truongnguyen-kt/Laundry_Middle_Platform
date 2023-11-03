using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IOrderRepository
    {
        public IList<Order> findAllOrders();

        public Order findOrderById(int orderId);

        public bool updateOrder(Order order, int orderId);
        public bool createOrder(Order order);
        public bool deleteOrder(int orderId);
        public Order findOrderByBasedOnSpecificFields(Order order);
    }
}
