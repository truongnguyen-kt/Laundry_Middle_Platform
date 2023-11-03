using BusinessObjects.Models;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Repository.Implement;
using Repository.Interface;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void storeOrderDetail(int storeId, string customer_email, List<Tuple<String, Double>> customer_kg)
        {
            DateTime startDateTime = DateTime.Now;
            DateTime finishDateTime = DateTime.Now;
            double totalVolume = 0;
            bool OrderStatus = false;
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

                OrderDetail orderDetail = new OrderDetail(newOrder.OrderId, type.TypeId, volume, orderDetailStatus);
                orderDetailRepository.createOrderDetail(orderDetail);
            }
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

                IList<int> listPerformancesMachine = washingMachines
                                    .Select(wm => wm.Performmance)
                                    .ToList();  // Get All Performance of Machine in one Store

                IList<double> listRequireCustomer = orderDetails
                                    .Select(od => od.Volume.HasValue ? od.Volume.Value : 0)
                                    .ToList();

                listPerformancesMachine.OrderBy(x => x).ToList(); 
                listRequireCustomer.OrderBy(x => x).ToList(); 

            }
        }

        //private int findTheClosetKg(int customer_kg, IList<int> listPerformancesMachine, HashSet<int> unorderedSet)
        //{

        //}
    }
}
