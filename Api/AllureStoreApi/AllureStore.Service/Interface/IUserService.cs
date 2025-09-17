using AllureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Service.Interface
{
    public interface IUserService
    {
        int InsertOrUpdateUser(UserModel user);
        UserModel VerifyUser(string email, string password);
        IEnumerable<UserModel> GetAllUser();
        IEnumerable<AdminNavItemModel> GetAdminNavItems();
    }
}
