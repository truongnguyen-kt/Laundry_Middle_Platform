using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TypeDAO
    {
        private static TypeDAO instance = null;
        private static readonly object instanceLock = new object();
        public static TypeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TypeDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<BusinessObjects.Models.Type> findAllTypes()
        {
            var Dbcontext = new LaundryMiddlePlatformContext();
            return Dbcontext.Types.ToList();
        }

        public BusinessObjects.Models.Type findTypeById(int typeid)
        {
            BusinessObjects.Models.Type type = null;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                type = Dbcontext.Types.FirstOrDefault(o => o.TypeId == typeid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return type;
        }

        public bool updateType(BusinessObjects.Models.Type order, int orderId)
        {
            BusinessObjects.Models.Type oldOrder = null;
            bool check = false;
            try
            {
                oldOrder = findTypeById(orderId);
                if (oldOrder != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Entry<BusinessObjects.Models.Type>(order).State = EntityState.Modified;
                    Dbcontext.SaveChanges();
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

        public bool deleteType(int orderId)
        {
            bool check = false;
            try
            {
                BusinessObjects.Models.Type order = findTypeById(orderId);
                if (order != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Types.Remove(order);
                    Dbcontext.SaveChanges();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }

        public bool createType(BusinessObjects.Models.Type order)
        {
            bool check = false;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                Dbcontext.Types.Add(order);
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
