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

        public IEnumerable<UserModel> GetUsers()
        {
            try
            {
                var users = _context.Users.Where(c => c.Deleted == false).Select(c => new UserModel 
                { 
                    Id = c.Id,
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                }).ToList();
                return users;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<AdminNavItemModel> GetAdminNavItems()
        {
            try
            {
                var item = _context.AdminNavItems.Where(c => c.Enabled == true && c.ParentId == 0).Select(c => new AdminNavItemModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Url = c.Url,
                    icon = c.icon,
                    ParentId = c.ParentId,
                    SortOrder = c.SortOrder,
                    children = _context.AdminNavItems.Where(x => x.Enabled == true && x.ParentId != c.ParentId).Select(child => new AdminNavItemModel
                    {
                        Id = child.Id,
                        Name = child.Name,
                        Url = child.Url,
                        icon = child.icon,
                        ParentId = child.ParentId,
                        SortOrder = child.SortOrder,
                    }).OrderBy(o => o.SortOrder).ToList()
                }).ToList();
                return item;
            }
            catch(Exception )
            {
                throw;
            }
        }
    }
}
