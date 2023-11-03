using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IStoreRepository
    {
        public IList<Store> GetAllStores();
        public Store GetStoreById(int id);
        public bool AddStore(Store store);
        public bool DeleteStore(int storeId);
        public bool UpdateStore(Store store, int storeId);
    }
}
