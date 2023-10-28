using DataAccess;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TypeRepository : ITypeRepository
    {
        public IList<BusinessObjects.Models.Type> findAllTypes() => TypeDAO.Instance.findAllTypes();

        public BusinessObjects.Models.Type findTypeById(int orderId) => TypeDAO.Instance.findTypeById(orderId);

        public bool updateType(BusinessObjects.Models.Type order, int orderId) => TypeDAO.Instance.updateType(order, orderId);
        public bool createType(BusinessObjects.Models.Type order) => TypeDAO.Instance.createType(order);
        public bool deleteType(int orderId) => TypeDAO.Instance.deleteType(orderId);
    }
}
