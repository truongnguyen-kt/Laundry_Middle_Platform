using BusinessObjects.Models;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class MachineRepository : IMachineRepository
    {
        public IList<WashingMachine> GetWashingMachines() => MachineDAO.Instance.GetWashingMachines();
        public bool AddWashingMachine(WashingMachine washingMachine) => MachineDAO.Instance.AddWashingMachine(washingMachine);
        public bool UpdateWashingMachine(WashingMachine washingMachine, int washingMachineId) => MachineDAO.Instance.UpdateWashingMachine(washingMachine, washingMachineId);
        public bool DeleteWashingMachine(int washingMachineId) => MachineDAO.Instance.DeleteWashingMachine(washingMachineId);
        public WashingMachine GetWashingMachineById(int washingMachineId) => MachineDAO.Instance.GetWashingMachineById(washingMachineId);

        public IList<WashingMachine> GetWashingMachinesByStoreId(int storeId) => MachineDAO.Instance.GetWashingMachinesByStoreId(storeId);
    }
}
