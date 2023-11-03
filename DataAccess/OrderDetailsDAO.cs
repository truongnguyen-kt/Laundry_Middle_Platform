using BusinessObjects.Models;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailsDAO
    {
        private static OrderDetailsDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDetailsDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailsDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<OrderDetail> findAllOrderDetails()
        {
            var dbContext = new LaundryMiddlePlatformContext();
            return dbContext.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Type)
                .ToList();
        }

        public OrderDetail findOrderDetailById(int orderDetailId)
        {
            OrderDetail orderDetail = null;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                orderDetail = dbContext.OrderDetails
                    .Include(o => o.Order)
                    .Include(o => o.Type)
                    .FirstOrDefault(o => o.OrderDetailId == orderDetailId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        public bool updateOrderDetail(OrderDetail orderDetail, int orderDetailId)
        {
            OrderDetail oldOrderDetail = null;
            bool check = false;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                oldOrderDetail = dbContext.OrderDetails
                    .Include(o => o.Order)
                    .Include(o => o.Type)
                    .FirstOrDefault(detail => detail.OrderId == orderDetailId);
                if (oldOrderDetail != null)
                {

                    dbContext.Entry<OrderDetail>(orderDetail).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
                throw new Exception(ex.Message);
            }
            return check;
        }

        public bool deleteOrderDetail(int orderDetailId)
        {
            bool check = false;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                OrderDetail orderDetail = dbContext.OrderDetails.FirstOrDefault(o => o.OrderDetailId == orderDetailId);
                if (orderDetail != null)
                {
                    dbContext.OrderDetails.Remove(orderDetail);
                    dbContext.SaveChanges();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }

        public bool createOrderDetail(OrderDetail orderDetail)
        {
            bool check = false;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                dbContext.OrderDetails.Add(orderDetail);
                dbContext.SaveChanges();
                check = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }

        public IList<OrderDetail> findAllOrderDetailsByOrderId(int orderId)
        {
            IList<OrderDetail> orderDetails = null;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                orderDetails = dbContext.OrderDetails
                    .Include(o => o.Order)
                    .Include(o => o.Type)
                    .Where(o => o.OrderId == orderId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }
    }
}
