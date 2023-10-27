using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static object instanceLook = new object();

        public static RoleDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }

                    return instance;
                }
            }
        }

        readonly LaundryMiddlePlatformContext _context = new LaundryMiddlePlatformContext();


        public List<Role?> GetAllRole()
        {
            return _context.Roles.ToList();
        }

        public void DeleteRole(int id)
        {
            var role = _context.Roles.Where(c => c.RoleId == id).ToList()[0];
            _context.Roles.Remove(role);
            _context.SaveChanges();

        }

        public void AddRole(Role role)
        {
            var maxId = _context.Roles.Max(c => c.RoleId);
            role.RoleId = maxId + 1;

            _context.Roles.Add(role);
            _context.SaveChanges();
        }
        public Role GetRoleById(int id)
        {
            var tmp = _context.Roles.Where(c => c.RoleId == id).ToList();
            if (tmp.Count > 0)
            {
                return tmp[0];
            }

            return null;
        }
        public void UpdateRole(Role role)
        {
            var existingRole = _context.Users.Find(role.RoleId);
            if (existingRole != null)
            {
                _context.Entry(existingRole).State = EntityState.Detached;
            }

            _context.Update(role);
            _context.SaveChanges();
        }


    }
}
