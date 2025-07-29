using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PizzaHub.Core.Configuration;
using PizzaHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Core.DB_Context
{
    public class PizzaHubContext: DbContext
    {
        public PizzaHubContext(DbContextOptions<PizzaHubContext> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMapConfig());

        }
    }
}
