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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public OrderDetail findOrderDetaiById(int orderDetailId) => OrderDetailsDAO.Instance.findOrderDetailById(orderDetailId);

        public bool updateOrderDetail(OrderDetail orderDetail, int orderDetailId) => OrderDetailsDAO.Instance.updateOrderDetail(orderDetail, orderDetailId);

        public bool createOrderDetail(OrderDetail orderDetail) => OrderDetailsDAO.Instance.createOrderDetail(orderDetail);

        public IList<OrderDetail> findAllOrderDetails() => OrderDetailsDAO.Instance.findAllOrderDetails();

        public bool deleteOrderDetail(int orderDetailId) => OrderDetailsDAO.Instance.deleteOrderDetail(orderDetailId);
        public IList<OrderDetail> findAllOrderDetailsByOrderId(int orderId) => OrderDetailsDAO.Instance.findAllOrderDetailsByOrderId(orderId);
    }
}
