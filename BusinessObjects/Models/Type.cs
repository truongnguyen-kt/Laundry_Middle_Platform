using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Type
    {
        public Type()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
