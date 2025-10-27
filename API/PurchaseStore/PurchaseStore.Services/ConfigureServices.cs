using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchaseStore.Core.DB_Context;
using PurchaseStore.Core.Entities;
using PurchaseStore.Repository.Implementation;
using PurchaseStore.Repository.Interface;
using PurchaseStore.Services.Implementation;
using PurchaseStore.Services.Interface;

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
            services.AddScoped<IRepository<AdminRole>, Repository<AdminRole>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<SubCategory>, Repository<SubCategory>>();





            // Repo
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<I_ImageRepo, ImageRepo>();




            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<I_ImageService, ImageService>();



        }
    }
}
