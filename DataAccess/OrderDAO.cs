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
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<Order> findAllOrders()
        {
            var dbContext = new LaundryMiddlePlatformContext();
            return dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .Include(o => o.Store)
                .ToList();
        }

        public Order findOrderById(int orderId)
        {
            Order order = null;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                order = dbContext.Orders
                    .Include(o => o.Store)
                    .Include(o => o.Customer)
                    .Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderId == orderId);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        public bool updateOrder(Order order, int orderId)
        {
            Order oldOrder = null;
            bool check = false;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                oldOrder = dbContext.Orders
                    .AsNoTracking()
                    .Include(o => o.Customer)  
                    .Include(o => o.OrderDetails)
                    .Include(o => o.Store)
                    .FirstOrDefault(o => o.OrderId == orderId);
                if(oldOrder != null)
                {
                    dbContext.Entry<Order>(order).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    check = true;
                }
            }
            catch(Exception ex) 
            {
                check = false;
                throw new Exception(ex.Message);
            }
            return check;
        }

        public bool deleteOrder(int orderId)
        {
            bool check = false;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                Order order = dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    dbContext.Orders.Remove(order);
                    dbContext.SaveChanges();
                    check = true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }

        public bool createOrder(Order order)
        {
            bool check = false;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                dbContext.Orders.Add(order);
                dbContext.SaveChanges();
                check = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }

        public Order findOrderByBasedOnSpecificFields(Order order)
        {
            Order FindOrder = null;
            try
            {
                var dbContext = new LaundryMiddlePlatformContext();
                FindOrder = dbContext.Orders
                    .FirstOrDefault(o => 
                            o.StartDateTime == order.StartDateTime && 
                            o.FinishDateTime == order.FinishDateTime &&
                            o.OrderStatus == order.OrderStatus &&
                            o.CustomerId == order.CustomerId &&
                            o.StoreId == order.StoreId
                            );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return FindOrder;
        }
    }
}
