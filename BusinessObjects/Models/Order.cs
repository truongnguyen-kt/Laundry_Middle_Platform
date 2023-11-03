using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
        public double? TotalPrice { get; set; }
        public string? OrderStatus { get; set; }
        public int? CustomerId { get; set; }
        public int? StoreId { get; set; }

        public virtual User? Customer { get; set; }
        public virtual Store? Store { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Order(DateTime startDateTime, DateTime finishDateTime, double? totalPrice, string? orderStatus, int? customerId, int? storeId)
        {
            StartDateTime = startDateTime;
            FinishDateTime = finishDateTime;
            TotalPrice = totalPrice;
            OrderStatus = orderStatus;
            CustomerId = customerId;
            StoreId = storeId;
        }
    }
}
