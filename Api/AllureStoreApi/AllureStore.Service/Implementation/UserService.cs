using AllureStore.Models;
using AllureStore.Repository.Interface;
using AllureStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public IEnumerable<AdminNavItemModel> GetAdminNavItems()
        {
            return _userRepo.GetAdminNavItems();
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            return _userRepo.GetAllUser();
        }

        public int InsertOrUpdateUser(UserModel user)
        {
            return _userRepo.InsertOrUpdateUser(user);  
        }

        public UserModel VerifyUser(string email, string password)
        {
            return _userRepo.VerifyUser(email, password);
        }
    }
}
