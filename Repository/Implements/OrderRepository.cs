using BusinessObjects.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IList<Order> findAllOrders() => OrderDAO.Instance.findAllOrders(); 

        public Order findOrderById(int orderId) => OrderDAO.Instance.findOrderById(orderId); 

        public bool updateOrder(Order order, int orderId) => OrderDAO.Instance.updateOrder(order, orderId);
        public bool createOrder(Order order) => OrderDAO.Instance.createOrder(order);
        public bool deleteOrder(int orderId) => OrderDAO.Instance.deleteOrder(orderId);
    }
}
