using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IMachineRepository
    {
        public IList<WashingMachine> GetWashingMachines();
        public bool AddWashingMachine(WashingMachine washingMachine);
        public bool UpdateWashingMachine(WashingMachine washingMachine, int washingMachineId);
        public bool DeleteWashingMachine(int washingMachineId);
        public WashingMachine GetWashingMachineById(int washingMachineId);

        public IList<WashingMachine> GetWashingMachinesByStoreId(int storeId);
    }
}
