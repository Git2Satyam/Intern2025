﻿using FoodApp.Core.Config;
using FoodApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.DB_Context
{
    public class FoodAppContext: DbContext
    {
        public FoodAppContext(DbContextOptions<FoodAppContext> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


        public void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMapConfig());
            builder.ApplyConfiguration(new UserMapConfig());

        }
    }
}
