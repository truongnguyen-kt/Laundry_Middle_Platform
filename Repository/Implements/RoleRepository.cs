using BusinessObjects.Models;
using DataAccess;
using Repository.IRepository;

namespace Repository.Implement
{
    public class RoleRepository : IRoleRepository
    {
        public void AddNewRole(Role newRole)
        {
            RoleDAO.Instance.AddRole(newRole);
        }

        public void DeleteRole(int userId)
        {
            RoleDAO.Instance.DeleteRole(userId);
        }

        public List<Role?> GetAllRole()
        {
            return RoleDAO.Instance.GetAllRole();
        }

        public Role GetRoleById(int id)
        {
            return RoleDAO.Instance.GetRoleById(id);
        }

        public void UpdateRole(Role role)
        {
            RoleDAO.Instance.UpdateRole(role);
        }

        
    }
}
