using PurchaseStore.Models;
using PurchaseStore.Repository.Interface;
using PurchaseStore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _roleRepo;
        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public int InsertOrUpdateRole(AdminRoleModel model)
        {
           return _roleRepo.InsertOrUpdateRole(model);
        }

        public IEnumerable<AdminRoleModel> GetAllRoles()
        {
            return _roleRepo.GetAllRoles();
        }

        public bool DeleteRole(string roleName)
        {
            return _roleRepo.DeleteRole(roleName);
        }
    }
}
