using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MachineDAO
    {
        private static MachineDAO instance = null;
        private static readonly object instanceLock = new object();
        public static MachineDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MachineDAO();
                    }
                    return instance;
                }
            }
        }

        public IList<WashingMachine> GetWashingMachines()
        {
            var Dbcontext = new LaundryMiddlePlatformContext();
            return Dbcontext.WashingMachines.ToList();
        }
        public bool AddWashingMachine(WashingMachine washingMachine)
        {
            bool check = false;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                Dbcontext.WashingMachines.Add(washingMachine);
                check = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }
        public bool UpdateWashingMachine(WashingMachine washingMachine, int washingMachineId)
        {
            WashingMachine oldWashingMachine = null;
            bool check = false;
            try
            {
                oldWashingMachine = GetWashingMachineById(washingMachineId);
                if (oldWashingMachine != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.Entry<WashingMachine>(oldWashingMachine).State = EntityState.Modified;
                    Dbcontext.SaveChanges();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
                throw new Exception(ex.Message);
            }
            return check;
        }
        public bool DeleteWashingMachine(int washingMachineId)
        {
            bool check = false;
            try
            {
                WashingMachine washingMachine = GetWashingMachineById(washingMachineId);
                if (washingMachine != null)
                {
                    var Dbcontext = new LaundryMiddlePlatformContext();
                    Dbcontext.WashingMachines.Remove(washingMachine);
                    Dbcontext.SaveChanges();
                    check = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return check;
        }
        public WashingMachine GetWashingMachineById(int washingMachineId)
        {
            WashingMachine washingMachine = null;
            try
            {
                var Dbcontext = new LaundryMiddlePlatformContext();
                washingMachine = Dbcontext.WashingMachines.FirstOrDefault(o => o.MachineId == washingMachineId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return washingMachine;
        }
    }
}
