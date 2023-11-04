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

        public  DateTime finishDateTime { get; set; } // max Date Time Finish

        public  double totalPrice { get; set; }

        public int storeId { get; set; }

        public List<int> machineID { get; set; }

        public OrderInvoice(int orderId, DateTime startDateTime, DateTime finishDateTime, double totalPrice, int storeId, List<int> machineID)
        {
            this.orderId = orderId;
            this.startDateTime = startDateTime;
            this.finishDateTime = finishDateTime;
            this.totalPrice = totalPrice;
            this.storeId = storeId;
            this.machineID = machineID;
        }

        public OrderInvoice() { }
        public override string ToString()
        {
            return $"Order ID: {orderId}\n" +
                   $"Start Date Time: {startDateTime.ToString()}\n" +
                   $"Finish Date Time: {finishDateTime.ToString()}\n" +
                   $"Total Price: {totalPrice}\n" +
                   $"Store ID: {storeId}\n" +
                   $"Machine IDs: {string.Join(", ", machineID)}";
        }
    }

}
