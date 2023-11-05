using BusinessObjects.Models;
using DataAccess;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class OrderRepository : IOrderRepository
    {
        public IList<Order> findAllOrders() => OrderDAO.Instance.findAllOrders(); 

        public Order findOrderById(int orderId) => OrderDAO.Instance.findOrderById(orderId); 

        public bool updateOrder(Order order, int orderId) => OrderDAO.Instance.updateOrder(order, orderId);
        public bool createOrder(Order order) => OrderDAO.Instance.createOrder(order);
        public bool deleteOrder(int orderId) => OrderDAO.Instance.deleteOrder(orderId);
        public Order findOrderByBasedOnSpecificFields(Order order) => OrderDAO.Instance.findOrderByBasedOnSpecificFields(order);

        public IList<Order> findAllOrderByStoreId(int storeId) => OrderDAO.Instance.findAllOrderByStoreId(storeId);
        public IList<Order> findAllOrderByCustomerId(int customerId) => OrderDAO.Instance.findAllOrderByCustomerId(customerId);

        public IList<Order> findAllOrderBetweenStartDateTimeAndStartEndTime(DateTime startDateTime, DateTime endDateTime) =>
            OrderDAO.Instance.findAllOrderBetweenStartDateTimeAndStartEndTime(startDateTime, endDateTime);

        public IList<Order> findAllOrderBetweenStartDateTimeAndStartEndTimeAndCustomerId(DateTime startDateTime, DateTime endDateTime, int customerId) =>
             OrderDAO.Instance.findAllOrderBetweenStartDateTimeAndStartEndTimeAndCustomerId(startDateTime, endDateTime, customerId);
    }
}
