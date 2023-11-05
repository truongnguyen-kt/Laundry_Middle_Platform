using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class WashingMachine
    {

        public int MachineId { get; set; }
        public string MachineName { get; set; } = null!;
        public double Performmance { get; set; }
        public bool? Status { get; set; }
        public int? StoreId { get; set; }

        public virtual Store? Store { get; set; }

        public WashingMachine(string MachineName, double Performance, bool? Status, int? StoreId)
        {
            this.MachineName = MachineName;
            this.Performmance = Performance;
            this.Status = Status;
            this.StoreId = StoreId;
        }

        public WashingMachine() { }
    }
}
