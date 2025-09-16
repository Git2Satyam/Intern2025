using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchaseStore.Core.DB_Context;
using PurchaseStore.Core.Entities;
using PurchaseStore.Repository.Implementation;
using PurchaseStore.Repository.Interface;
using PurchaseStore.Services.Implementation;
using PurchaseStore.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseStore.Services
{
    public static class ConfigureServices
    {
        public static void RegisterService(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PurchaseAppContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("Db_Connection"));
            });

            services.AddScoped<DbContext, PurchaseAppContext>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<AdminNavtItem>, Repository<AdminNavtItem>>();



            // Repo
            services.AddScoped<IUserRepo, UserRepo>();  

            // Services
            services.AddScoped<IUserService, UserService>();
        }
    }
}
