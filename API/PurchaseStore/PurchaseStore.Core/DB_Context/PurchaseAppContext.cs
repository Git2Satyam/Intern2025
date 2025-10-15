using Microsoft.EntityFrameworkCore;
using PurchaseStore.Core.Config;
using PurchaseStore.Core.Entities;

namespace PurchaseStore.Core.DB_Context
{
    public class PurchaseAppContext: DbContext
    {
        public PurchaseAppContext(DbContextOptions<PurchaseAppContext> option): base(option)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AdminNavtItem> AdminNavItems { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }


        public void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMapConfig());
        }
    }
}
