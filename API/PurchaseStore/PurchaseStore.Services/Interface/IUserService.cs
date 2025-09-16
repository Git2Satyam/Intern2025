using PurchaseStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services.Interface
{
    public interface IUserService
    {
        int InsertOrUpdateUser(UserModel user);
        UserModel AuthenticateUser(string email, string password);
        IEnumerable<UserModel> GetUsers();

        IEnumerable<AdminNavItemModel> GetAdminNavItems();
    }
}
