using BusinessObjects.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class StoreRepository : IStoreRepository
    {
        public IList<Store> GetAllStores() => StoreDAO.Instance.GetAllStores();
        public Store GetStoreById(int id) => StoreDAO.Instance.GetStoreById(id);
        public bool AddStore(Store store) => StoreDAO.Instance.AddStore(store);
        public bool DeleteStore(int storeId) => StoreDAO.Instance.DeleteStore(storeId);
        public bool UpdateStore(Store store, int storeId) => StoreDAO.Instance.UpdateStore(store, storeId);

        public IList<Store> GetAllStoreID() => StoreDAO.Instance.GetAllStoresID();
    }
}
