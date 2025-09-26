using PurchaseStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Repository.Interface
{
    public interface IRoleRepo
    {
        int InsertOrUpdateRole(AdminRoleModel model);
        IEnumerable<AdminRoleModel> GetAllRoles();
        bool DeleteRole(string roleName);
    }
}
