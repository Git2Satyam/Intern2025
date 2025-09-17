using AllureStore.Core.Config;
using AllureStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllureStore.Core.DB_Context
{
    public class AllureAppContext: DbContext
    {
        public AllureAppContext(DbContextOptions<AllureAppContext> option): base(option)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdminNavItem> AdminNavItems { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }

        public void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMapConfig());
        }
    }
}
