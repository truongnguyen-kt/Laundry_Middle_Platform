using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? TypeId { get; set; }
        public double? Volume { get; set; }
        public bool? OrderDetailStatus { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Type? Type { get; set; }

        public OrderDetail(int? orderId, int? typeId, double? volume, bool? orderDetailStatus)
        {
            OrderId = orderId;
            TypeId = typeId;
            Volume = volume;
            OrderDetailStatus = orderDetailStatus;
        }
    }
}
