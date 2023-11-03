using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        public IList<OrderDetail> findAllOrderDetails();

        public OrderDetail findOrderDetaiById(int orderDetailId);

        public bool updateOrderDetail(OrderDetail orderDetail, int orderDetailId);
        public bool createOrderDetail(OrderDetail orderDetail);
        public bool deleteOrderDetail(int orderDetailId);

        public IList<OrderDetail> findAllOrderDetailsByOrderId(int orderId);
    }
}
