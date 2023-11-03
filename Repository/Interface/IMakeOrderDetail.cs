using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IMakeOrderDetail
    {
        public int storeOrderDetail(int storeId, string customer_email, List<Tuple<String, Double>> customer_kg);
        public OrderInvoice CalculateOrderTimeLine(int orderId);
    }
}
