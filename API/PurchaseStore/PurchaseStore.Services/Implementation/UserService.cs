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
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public UserModel AuthenticateUser(string email, string password)
        {
            return _userRepo.AuthenticateUser(email, password);
        }

        public int InsertOrUpdateUser(UserModel user)
        {
            return _userRepo.InsertOrUpdateUser(user);
        }
    }
}
