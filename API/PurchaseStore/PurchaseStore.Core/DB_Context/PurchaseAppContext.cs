using Microsoft.EntityFrameworkCore;
using PurchaseStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Core.DB_Context
{
    public class PurchaseAppContext: DbContext
    {
        public PurchaseAppContext(DbContextOptions<PurchaseAppContext> option): base(option)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
