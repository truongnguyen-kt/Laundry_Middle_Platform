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
            var Dbcontext = new LaundryMiddlePlatformContext();
            return Dbcontext.Orders.ToList();
        }

        public Order findOrderById(int orderId)
        {
            Order order = null;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                order = Dbcontext.Orders.FirstOrDefault(o => o.OrderId == orderId);
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
                oldOrder = findOrderById(orderId);  
                if(oldOrder != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Entry<Order>(order).State = EntityState.Modified;
                    Dbcontext.SaveChanges();
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
                Order order = findOrderById(orderId);
                if(order != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Orders.Remove(order);
                    Dbcontext.SaveChanges();
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
                var Dbcontext = new LaundryMiddlePlatformContext();
                Dbcontext.Orders.Add(order);
                check = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }
    }
}
