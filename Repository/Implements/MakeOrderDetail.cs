using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repository.Implements
{
    public class MakeOrderDetail : IMakeOrderDetail
    {
        private IStoreRepository storeRepository = new StoreRepository();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository(); 
        private IOrderRepository orderRepository = new OrderRepository();   
        private ITypeRepository typeRepository = new TypeRepository();
        private IUserRepository userRepository = new UserRepository();
        private IMachineRepository machineRepository = new MachineRepository();
        public MakeOrderDetail() {
        }

        public int storeOrderDetail(int storeId, string customer_email, List<Tuple<String, Double>> customer_kg)
        {
            DateTime startDateTime = DateTime.Now;
            DateTime finishDateTime = DateTime.Now;
            double totalVolume = 0;
            string OrderStatus = "PENDING";
            User user = userRepository.findUserByEmail(customer_email);
            Order order = new Order(
                    startDateTime, finishDateTime, totalVolume, OrderStatus, user.UserId, storeId   
            );
            orderRepository.createOrder(order);
            Order newOrder = orderRepository.findOrderByBasedOnSpecificFields(order);
            
            for(int i = 0; i < customer_kg.Count; i++)
            {
                Tuple<String, Double> customer = customer_kg[i];
                String typeClothes = customer.Item1;
                Double KgPerClothes = customer.Item2;

                BusinessObjects.Models.Type type = typeRepository.findTypeByName(typeClothes);
                bool orderDetailStatus = false;
                double volume = KgPerClothes;

                OrderDetail orderDetail = new OrderDetail(newOrder.OrderId, type.TypeId, volume);
                orderDetailRepository.createOrderDetail(orderDetail);
            }

            return newOrder.OrderId;
        }

        private Tuple<int, double> findTheClosetKg(double customer_kg, List<Tuple<int, double>> listPerformancesMachine, HashSet<Tuple<int, double>> unorderedset)
        {
            double maxCloset = double.MaxValue;
            double minCloset = double.MinValue;
            Tuple<int, double> maxTuple = null;
            Tuple<int, double> minTuple = null; 

            for (int i = 0; i < listPerformancesMachine.Count; i++)
            {
                if (listPerformancesMachine[i].Item2 >= customer_kg && listPerformancesMachine[i].Item2 < maxCloset
                    && !unorderedset.Contains(listPerformancesMachine[i]))
                {
                    maxCloset = listPerformancesMachine[i].Item2;
                    maxTuple = listPerformancesMachine[i];
                }

                if (listPerformancesMachine[i].Item2 < customer_kg && listPerformancesMachine[i].Item2 > minCloset
                    && !unorderedset.Contains(listPerformancesMachine[i]))
                {
                    minCloset = listPerformancesMachine[i].Item2;
                    minTuple = listPerformancesMachine[i];
                }
            }
            if (maxCloset != double.MaxValue)
            {
                unorderedset.Add(maxTuple);
                return maxTuple;
            }
            if (minCloset != double.MinValue)
            {
                unorderedset.Add(minTuple);
                return minTuple;
            }
            return null;
        }

        private DateTime calculateNewEndTime(DateTime oldFinishDateTime, int hours)
        {
            int oldYear = oldFinishDateTime.Year;
            int oldMonth = oldFinishDateTime.Month;
            int oldDay = oldFinishDateTime.Day;
            int oldHour = oldFinishDateTime.Hour + hours;
            int oldMinute = oldFinishDateTime.Minute;
            int oldSecond = oldFinishDateTime.Second;
            int oldMillisecond = oldFinishDateTime.Millisecond;

            return new DateTime(oldYear, oldMonth, oldDay, oldHour, oldMinute, oldSecond, oldMillisecond);
        }

        public OrderInvoice CalculateOrderTimeLine(int orderId)
        {
            Order order = orderRepository.findOrderById(orderId);
            if (order != null)
            {
                int store_id = (int)order.StoreId;

                Store store = storeRepository.GetStoreById(store_id); 

                IList<WashingMachine> washingMachines = machineRepository.GetWashingMachinesByStoreId(store_id);
                IList<OrderDetail> orderDetails = orderDetailRepository.findAllOrderDetailsByOrderId(orderId);

                // store Machine Id, Machine Performance
                List<Tuple<int, double>> listPerformancesMachine = new List<Tuple<int, double>>();

                foreach (WashingMachine washingMachine in washingMachines)
                {
                    listPerformancesMachine.Add(new Tuple<int, double>(washingMachine.MachineId, washingMachine.Performmance));
                }

                
                IList<double> listRequireCustomer = orderDetails
                                    .Select(od => od.Volume.HasValue ? od.Volume.Value : 0)
                                    .ToList();

                // sort performance by ascending 
                listPerformancesMachine.Sort((x, y) => x.Item2.CompareTo(y.Item2));
                listRequireCustomer = listRequireCustomer.OrderBy(x => x).ToList();
                HashSet<Tuple<int, double>> unorderedset = new HashSet<Tuple<int, double>>();
                double totalCustomerKg = listRequireCustomer.Sum();
                double totalPrice = (double)totalCustomerKg * (double)store.Price;

                bool check1 = true;
                int n1 = listPerformancesMachine.Count - 1;
                int m1 = listRequireCustomer.Count - 1;
                while (n1 >= 0 && m1 >= 0)
                {
                    if (listPerformancesMachine[n1].Item2 > listRequireCustomer[m1])
                    {
                        check1 = false;
                        break;
                    }
                    n1--;
                    m1--;
                }
                List<DateTime> listDate = new List<DateTime>();
                List<int> listMachineId = new List<int>();
                List<Tuple<int, Tuple<double, double>>> result = new List<Tuple<int, Tuple<double, double>>>();
                DateTime maxDateTime;
                //result use to store machineId, store Performance of Machine and store Kg of Customer
                if (check1) 
                {

                    int n2 = listPerformancesMachine.Count - 1;
                    int m2 = listRequireCustomer.Count - 1;
                    while (n2 >= 0 && m2 >= 0)
                    {
                        Tuple<double, double> pair_kg = new Tuple<double, double>(listPerformancesMachine[n2].Item2, listRequireCustomer[m2]);
                        Tuple<int, Tuple<double, double>> store_data = new Tuple<int, Tuple<double, double>>(listPerformancesMachine[n2].Item1, pair_kg);
                        result.Add(store_data);
                        n2--;
                        m2--;
                    }
                    result.Reverse();
                   
                    foreach(Tuple<int, Tuple<double, double>> res in result)
                    {
                        int hours = (int)Math.Ceiling(res.Item2.Item2 / res.Item2.Item1);
                        DateTime oldFinishDateTime = order.FinishDateTime;
                        DateTime newFinishDateTime = calculateNewEndTime(oldFinishDateTime, hours);
                        listDate.Add(newFinishDateTime);

                        Console.WriteLine("Machine Id: " + res.Item1 + 
                            " Performance Machine : " + res.Item2.Item1 + 
                            " Customer Kg: " + res.Item2.Item2 +
                            " Time for Laundry: " + hours + 
                            " Old FinishDateTime: " + oldFinishDateTime +
                            " New FinishDateTime: " + newFinishDateTime);
                        listMachineId.Add(res.Item1);
                    }
                    maxDateTime = listDate.Max();
                }
                else
                {
                    foreach(double customer_kg in listRequireCustomer)
                    {
                        Tuple<int, double> machine = findTheClosetKg(customer_kg, listPerformancesMachine, unorderedset);
                        int hours = (int)Math.Ceiling(customer_kg / machine.Item2);
                        DateTime oldFinishDateTime = order.FinishDateTime;
                        DateTime newFinishDateTime = calculateNewEndTime(oldFinishDateTime, hours);
                        listDate.Add(newFinishDateTime);

                        Console.WriteLine("Machine Id: " + machine.Item1 +
                           " Performance Machine : " + machine.Item2 +
                           " Customer Kg: " + customer_kg +
                           " Time for Laundry: " + hours +
                           " Old FinishDateTime: " + oldFinishDateTime +
                           " New FinishDateTime: " + newFinishDateTime);
                        listMachineId.Add(machine.Item1);
                    }
                    maxDateTime = listDate.Max();
                }
                return new OrderInvoice(orderId, order.StartDateTime, maxDateTime, totalPrice, store_id, listMachineId);
            }
            return new OrderInvoice();
        }
        public void CancelOrder(int orderId)
        {
            Order order = orderRepository.findOrderById(orderId);
            if (order != null)
            {
                order.OrderStatus = "CANCEL";
                bool result = orderRepository.updateOrder(order, orderId);
            }
        }

        /*public void OrderLaundry(OrderInvoice orderInvoice)
        {
            Order order = orderRepository.findOrderById(orderInvoice.orderId);
            Store store = storeRepository.GetStoreById(orderInvoice.storeId);
            List<int> machineID = orderInvoice.machineID;
            if(order != null && store != null)
            {
                order.OrderStatus = "PROCESSING";
                orderRepository.updateOrder(order, orderInvoice.orderId);
                for(int i = 0; i <  machineID.Count; i++) 
                {
                    WashingMachine washingMachine = machineRepository.GetWashingMachineById(machineID[i]);
                    if(washingMachine != null)
                    {
                        washingMachine.Status = false;
                        machineRepository.UpdateWashingMachine(washingMachine, machineID[i]);
                    }
                }
            }
        }*/

        public void OrderLaundry(OrderInvoice orderInvoice)
        {
            Order order = orderRepository.findOrderById(orderInvoice.orderId);
            Store store = storeRepository.GetStoreById(orderInvoice.storeId);
            List<int> machineID = orderInvoice.machineID;
            if (order != null && store != null)
            {
                order.StartDateTime = orderInvoice.startDateTime;
                order.FinishDateTime = orderInvoice.finishDateTime;
                order.OrderStatus = "PROCESSING";
                orderRepository.updateOrder(order, orderInvoice.orderId);
                for (int i = 0; i < machineID.Count; i++)
                {
                    WashingMachine washingMachine = machineRepository.GetWashingMachineById(machineID[i]);
                    if (washingMachine != null)
                    {
                        washingMachine.Status = false;
                        machineRepository.UpdateWashingMachine(washingMachine, machineID[i]);
                    }
                }
            }
        }
    }
}
