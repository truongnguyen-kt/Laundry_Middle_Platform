using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static object instanceLook = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }

                    return instance;
                }
            }
        }

        readonly LaundryMiddlePlatformContext _context = new LaundryMiddlePlatformContext();

        public List<User?> GetAllUser()
        {
            return _context.Users.ToList();
        }

        public void DeleteUser(int id)
        {
            var user = _context.Users.Where(c => c.UserId == id).ToList()[0];
            _context.Users.Remove(user);
            _context.SaveChanges();

        }
        public void AddUser(User user)
        {
            /*var maxId = _context.Users.Max(c => c.UserId);
            user.UserId = maxId + 1;*/
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User?> GetCustomerByEmail(string Email)
        {
            return _context.Users.Where(cus => cus.Email.ToUpper().Contains(Email.ToUpper())).ToList();
        }
        public User GetCustomerById(int id)
        {
            var tmp = _context.Users.Where(c => c.UserId == id).ToList();
            if (tmp.Count > 0)
            {
                return tmp[0];
            }

            return null;
        }
        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.UserId);
            if (existingUser != null)
            {
                _context.Entry(existingUser).State = EntityState.Detached;
            }

            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
