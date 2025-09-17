using AllureStore.Core.DB_Context;
using AllureStore.Core.Entities;
using AllureStore.Models;
using AllureStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Repository.Implementation
{
    public class UserRepo : Repository<User>, IUserRepo
    {
        AllureAppContext _context
        {
            get
            {
                return _dbContext as AllureAppContext;
            }
        }

        public UserRepo(AllureAppContext _db): base(_db)
        {
        }

        
        public int InsertOrUpdateUser(UserModel user)
        {
            int result = 0;
            try
            {
                var userExist = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                if (userExist != null)
                {
                    
                }
                else
                {
                    var adduser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.Password,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        PasswordExpiryDate = DateTime.Now.AddMonths(6),
                        Address = user.Address,
                        PhoneNumber = user.PhoneNumber,
                    };
                    _context.Users.Add(adduser);
                    _context.SaveChanges();
                    result = 1;
                }
                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UserModel VerifyUser(string email, string password)
        {
            var ml = new UserModel();
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password == password && x.PasswordExpiryDate > DateTime.Now);
                if (user != null)
                {
                    ml.Id = user.Id;
                    ml.FirstName = user.FirstName;
                    ml.LastName = user.LastName;
                    ml.Email = user.Email;
                }
                return ml;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserModel> GetAllUser()
        {
            try
            {
                var users = _context.Users.Where(c => c.IsDeleted == false).Select(x => new UserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
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
                var items = _context.AdminNavItems.Where(c => c.Enabled == true && c.ParentId == 0).Select(t => new AdminNavItemModel
                {
                    Id = t.Id,
                    ParentId = t.ParentId,
                    Name = t.Name,
                    Url = t.Url,
                    icon = t.icon,
                    SortOrder = t.SortOrder,
                    Children = _context.AdminNavItems.Where(x => x.Enabled == true && x.ParentId != t.ParentId).Select(item => new AdminNavItemModel
                    {
                        Id = item.Id,
                        ParentId = item.ParentId,
                        Name = item.Name,
                        Url = item.Url,
                        icon = item.icon,
                        SortOrder= item.SortOrder,
                    }).ToList(),
                }).ToList();
                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
