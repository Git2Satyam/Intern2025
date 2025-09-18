using AllureStore.Core.DB_Context;
using AllureStore.Core.Entities;
using AllureStore.Models;
using AllureStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Repository.Implementation
{
    public class RoleRepo : Repository<AdminRole>, IRoleRepo
    {
        AllureAppContext _context
        {
            get
            {
                return _dbContext as AllureAppContext;
            }
        }
        public RoleRepo(AllureAppContext _db): base(_db)
        {
        }

        public int InsertOrUpdateRole(AdminRoleModel model)
        {
            int result = 0;
            try
            {
                var roleExist = _context.AdminRoles.FirstOrDefault(c => c.RoleName.Equals(model.RoleName));
                if (roleExist != null)
                {
                    roleExist.IsDeleted = false;
                    roleExist.View = model.View;
                    roleExist.Edit = model.Edit;

                    _context.SaveChanges();
                    result = 1;
                }
                else
                {
                    var role = new AdminRole()
                    {
                        RoleName = model.RoleName,
                        View = model.View,
                        Edit = model.Edit,
                        IsDeleted = false
                    };
                    _context.AdminRoles.Add(role);
                    _context.SaveChanges();
                    result = 1;
                }
                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<AdminRoleModel> GetAllRoles()
        {
            try
            {
                var roles = _context.AdminRoles.Where(r => r.IsDeleted == false).Select(r => new AdminRoleModel
                {
                    RoleName = r.RoleName,
                    View = r.View,
                    Edit =r.Edit
                }).ToList();
                return roles;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
