using BusinessObjects.Models;
using DataAccess;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        public void AddNewCustomer(User newCustomer)
        {
            UserDAO.Instance.AddUser(newCustomer);
        }

        public void DeleteUser(int userId)
        {
            UserDAO.Instance.DeleteUser(userId);
        }

        public List<User?> GetAllUsers()
        {
            return UserDAO.Instance.GetAllUser();
        }

        public User GetCustomerByEmailAndPassword(string email, string password)
        {
            return UserDAO.Instance.GetCustomerByEmailAndPassword(email, password);
        }

        public List<User?> GetUserByEmail(string email)
        {
           return UserDAO.Instance.GetCustomerByEmail(email);
        }

        public User GetUserById(int id)
        {
            return UserDAO.Instance.GetCustomerById(id);
        }

        public void UpdateUser(User user)
        {           
            UserDAO.Instance.UpdateUser(user);
        }
    }
}
