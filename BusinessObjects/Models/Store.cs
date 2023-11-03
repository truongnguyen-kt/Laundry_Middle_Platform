using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Store
    {
        public Store()
        {
            Orders = new HashSet<Order>();
            WashingMachines = new HashSet<WashingMachine>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WashingMachine> WashingMachines { get; set; }
    }
}
