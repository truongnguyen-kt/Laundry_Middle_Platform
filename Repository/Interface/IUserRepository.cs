using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        public void UpdateUser(User user);

        public bool DeleteUser(int userId);


        public List<User?> GetAllUsers();

        public User GetUserById(int id);

        public User findUserByEmail(string email);

        public void AddNewCustomer(User newCustomer);

        public List<User?> GetUserByEmail(string email);

        public User GetCustomerByEmailAndPassword(string email, string password);
    }
}
