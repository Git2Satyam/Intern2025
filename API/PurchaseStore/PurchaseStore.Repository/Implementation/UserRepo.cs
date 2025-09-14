using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using PurchaseStore.Core.DB_Context;
using PurchaseStore.Core.Entities;
using PurchaseStore.Models;
using PurchaseStore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Repository.Implementation
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        PurchaseAppContext _context
        {
            get
            {
                return _dbContext as PurchaseAppContext;
            }
        }
        public UserRepo(PurchaseAppContext _db): base(_db)
        {
        }

        public int InsertOrUpdateUser(UserModel user)
        {
            int result = 0; 
            try
            {
                var userExist = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (userExist == null)
                {
                    var addUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Address = user.Address,
                        Password = user.Password,
                        PhoneNumber = user.PhoneNumber,
                        PasswordExpiryDate = DateTime.Now.AddMonths(6),
                        CreatedDate = DateTime.Now,
                        Deleted = false,
                    };
                    _context.Users.Add(addUser);  
                    _context.SaveChanges();
                    result = 1;
                }
                else
                {

                }
                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public UserModel AuthenticateUser(string email, string password)
        {
            var currentTime = DateTime.Now;
            var userML = new UserModel();
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password && x.PasswordExpiryDate > currentTime);
                if(user != null)
                {
                    userML.Email = user.Email;
                    userML.FirstName = user.FirstName; 
                    userML.LastName = user.LastName;
                }
                return userML;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
