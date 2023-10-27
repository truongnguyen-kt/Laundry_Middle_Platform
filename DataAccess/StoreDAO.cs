using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StoreDAO 
    {
        private static StoreDAO instance = null;
        private static readonly object instanceLock = new object();
        public static StoreDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new StoreDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<Store> GetAllStores()
        {
            var Dbcontext = new LaundryMiddlePlatformContext();
            return Dbcontext.Stores.ToList();
        }
        public Store GetStoreById(int id)
        {
            Store store = null;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                store = Dbcontext.Stores.FirstOrDefault(o => o.StoreId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return store;
        }

        public bool AddStore(Store store)
        {
            bool check = false;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                Dbcontext.Stores.Add(store);
                check = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }

        public bool DeleteStore(int storeId)
        {
            bool check = false;
            try
            {
                Store store = GetStoreById(storeId);
                if (store != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Stores.Remove(store);
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
        public bool UpdateStore(Store store, int storeId)
        {
            Store oldStore = null;
            bool check = false;
            try
            {
                oldStore = GetStoreById(storeId);
                if (oldStore != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Entry<Store>(store).State = EntityState.Modified;
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
    }
}
