using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using Repository.IRepository;
using Repository.Implements;
using Repository.Interface;
using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using Validation;
using Repository;
using System.Collections;

namespace LaundryMidlePlatform.Pages.CustomerHomePage.OrderPage
{
    public class CustomerMakeOrderDetailModel : PageModel
    {
        private IUserRepository userRepository = new UserRepository();
        private Utils validation = new Utils();
        private IStoreRepository storeRepository = new StoreRepository();
        private ITypeRepository typeRepository = new TypeRepository();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private IMakeOrderDetail makeOrderDetail = new MakeOrderDetail();

        [BindProperty]
        public string Email { get; set; }
        public User User { get; set; }
        public Store store { get; set; }

        [BindProperty]
        public string Shirts { get; set; }

        [BindProperty]
        public string Pants { get; set; }

        [BindProperty]
        public string Other_Accessories { get; set; }
        public string Error { get; set; }
        public string Success { get; set; }

        [BindProperty]
        public int storeId { get; set; }

        public List<Tuple<String, Double>> customer_kg = new List<Tuple<String, Double>>();


        public void OnGet(int? id)
        {
            string email = HttpContext.Session.GetString("customerEmail");
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                User = userRepository.findUserByEmail(email);
                storeId = (int)id;
            }
        }

        public IActionResult OnPostAsync()
        {
            if (string.IsNullOrEmpty(Shirts) && string.IsNullOrEmpty(Pants) && string.IsNullOrEmpty(Other_Accessories)) 
            {
                Error = "You have to choose at least one type of Product to Landuary";
                return Page();
            }
            if(!string.IsNullOrEmpty(Shirts))
            {
                if (validation.CheckContainLetter(Shirts))
                {
                    Error = "Shirts Kg is not contain Letters";
                    return Page();
                }
            }

            if (!string.IsNullOrEmpty(Pants))
            {
                if (validation.CheckContainLetter(Pants))
                {
                    Error = "Pants Kg is not contain Letters";
                    return Page();
                }
            }

            if (!string.IsNullOrEmpty(Other_Accessories))
            {
                if (validation.CheckContainLetter(Other_Accessories))
                {
                    Error = "Other Accessories Kg is not contain Letters";
                    return Page();
                }
            }


            double shirts = -1;
            if(!string.IsNullOrEmpty(Shirts))
            {
                shirts = Double.Parse(Shirts);
                customer_kg.Add(new Tuple<String, Double>("SHIRTS", shirts));
            }
            double pants = -1;
            if (!string.IsNullOrEmpty(Pants))
            {
                pants = Double.Parse(Pants);
                customer_kg.Add(new Tuple<String, Double>("PANTS", pants));
            }
            double other_accessories = -1;
            if (!string.IsNullOrEmpty(Other_Accessories))
            {
                other_accessories = Double.Parse(Other_Accessories);
                customer_kg.Add(new Tuple<String, Double>("OTHER ACCESSORIES", other_accessories));
            }
            int OrderId = makeOrderDetail.storeOrderDetail(storeId, Email, customer_kg);
            return RedirectToPage("/CustomerHomePage/OrderPage/OrderTimeLine", new { OrderId = OrderId });
        }
    }
}
