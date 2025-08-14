using FoodApp.Core.DB_Context;
using FoodApp.Core.Entities;
using FoodApp.Repository.Implementation;
using FoodApp.Repository.Interface;
using FoodApp.Services.Implementation;
using FoodApp.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Services
{
    public static class ConfigureServices
    {
        public static void RegisterService(IServiceCollection _services, IConfiguration _config)
        {
            _services.AddDbContext<FoodAppContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("Db_Connection"));
            });

            _services.AddScoped<DbContext, FoodAppContext>();

            // Entities
            _services.AddScoped<IRepository<Product>, Repository<Product>>();
            _services.AddScoped<IRepository<User>, Repository<User>>();
            _services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            _services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();


            // Repository

            _services.AddScoped<IProductRepo, ProductRepo>();
            _services.AddScoped<ICartRepo, CartRepo>();

            // Services
            _services.AddScoped<IProductService, ProductService>();
            _services.AddScoped<ICartService, CartService>();


        }
    }
}
