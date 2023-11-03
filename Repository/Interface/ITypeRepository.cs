using DataAccess;

namespace Repository.Interface
{
    public interface ITypeRepository
    {
        public IList<BusinessObjects.Models.Type> findAllTypes();

        public BusinessObjects.Models.Type findTypeById(int orderId);

        public bool updateType(BusinessObjects.Models.Type order, int orderId);
        public bool createType(BusinessObjects.Models.Type order);
        public bool deleteType(int orderId);

        public BusinessObjects.Models.Type findTypeByName(string name);
    }
}