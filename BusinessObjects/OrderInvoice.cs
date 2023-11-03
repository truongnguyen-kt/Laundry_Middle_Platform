using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class OrderInvoice
    {
        public  int orderId {  get; set; }

        public  DateTime startDateTime { get; set; }

        public  DateTime finishDateTime { get; set; }

        public  double totalPrice { get; set; }

        public OrderInvoice(int orderId, DateTime startDateTime, DateTime finishDateTime, double totalPrice)
        {
            this.orderId = orderId;
            this.startDateTime = startDateTime;
            this.finishDateTime = finishDateTime;
            this.totalPrice = totalPrice;
        }

        public OrderInvoice() { }
    }

}
