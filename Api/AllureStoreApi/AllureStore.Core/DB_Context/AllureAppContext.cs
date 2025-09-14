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
    }
}
