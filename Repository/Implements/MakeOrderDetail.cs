using BusinessObjects.Models;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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

        private double findTheClosetKg(double customer_kg, IList<double> listPerformancesMachine, HashSet<double> unorderedset)
        {
            double maxCloset = double.MaxValue;
            double minCloset = double.MinValue;

            for (int i = 0; i < listPerformancesMachine.Count; i++)
            {
                if (listPerformancesMachine[i] >= customer_kg && listPerformancesMachine[i] < maxCloset
                    && !unorderedset.Contains(listPerformancesMachine[i]))
                {
                    maxCloset = listPerformancesMachine[i];
                }

                if (listPerformancesMachine[i] < customer_kg && listPerformancesMachine[i] > minCloset
                    && !unorderedset.Contains(listPerformancesMachine[i]))
                {
                    minCloset = listPerformancesMachine[i];
                }
            }
            if (maxCloset != double.MaxValue)
            {
                unorderedset.Add(maxCloset);
                return maxCloset;
            }
            if (minCloset != double.MinValue)
            {
                unorderedset.Add(minCloset);
                return minCloset;
            }
            return -1;
        }

        public void CalculateOrderTimeLine(int orderId)
        {
            Order order = orderRepository.findOrderById(orderId);
            if (order != null)
            {
                int store_id = (int)order.StoreId;

                Store store = storeRepository.GetStoreById(store_id); // Get the Store in which order you want to landuary

                IList<WashingMachine> washingMachines = machineRepository.GetWashingMachinesByStoreId(store_id); // Get list of machine with the performance per one machines
                IList<OrderDetail> orderDetails = orderDetailRepository.findAllOrderDetailsByOrderId(orderId); // Get list of OrderDetail Requirements

                IList<double> listPerformancesMachine = washingMachines
                                    .Select(wm => wm.Performmance)
                                    .ToList();  // Get All Performance of Machine in one Store

                IList<double> listRequireCustomer = orderDetails
                                    .Select(od => od.Volume.HasValue ? od.Volume.Value : 0)
                                    .ToList();

                listPerformancesMachine.OrderBy(x => x).ToList(); 
                listRequireCustomer.OrderBy(x => x).ToList();
                HashSet<Double> unorderedset = new HashSet<double>();

                bool check1 = true;
                int n1 = listPerformancesMachine.Count - 1;
                int m1 = listRequireCustomer.Count - 1;
                while (n1 >= 0 && m1 >= 0)
                {
                    if (listPerformancesMachine[n1] > listRequireCustomer[m1])
                    {
                        check1 = false;
                        break;
                    }
                    n1--;
                    m1--;
                }
                IList<Tuple<Double, Double>> result = new List<Tuple<Double, Double>>();
                if (check1) // trong qua trinh duyet qua tung phan tu k co gia tri nao cua customer > store
                {

                    int n2 = listPerformancesMachine.Count - 1;
                    int m2 = listRequireCustomer.Count - 1;
                    while (n2 >= 0 && m2 >= 0)
                    {
                        result.Add(new Tuple<double, double>(listPerformancesMachine[n2], listPerformancesMachine[m2]));
                        n1--;
                        m1--;
                    }
                    result.Reverse();
                    foreach(Tuple<Double, Double> kg in result)
                    {
                        Console.WriteLine("Store Kg: " + kg.Item1 + " Customer Kg:" + kg.Item2);
                    }
                }
                else
                {
                    foreach(double customer_kg in listRequireCustomer)
                    {
                        double ClosetKg = findTheClosetKg(customer_kg, listPerformancesMachine, unorderedset);
                        Console.WriteLine("Store Kg: " + ClosetKg + " Customer Kg:" + customer_kg);
                    }
                }
                

            }
        }

    }
}
