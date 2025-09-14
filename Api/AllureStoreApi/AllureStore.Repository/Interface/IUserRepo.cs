using AllureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Repository.Interface
{
    public interface IUserRepo
    {
        int InsertOrUpdateUser(UserModel user);
        UserModel VerifyUser(string email, string password);
        IEnumerable<UserModel> GetAllUser();


    }
}
