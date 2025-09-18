using AllureStore.Core.DB_Context;
using AllureStore.Core.Entities;
using AllureStore.Repository.Implementation;
using AllureStore.Repository.Interface;
using AllureStore.Service.Implementation;
using AllureStore.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AllureStore.Service
{
    public static class ConfigureServices
    {
        public static void RegisterService(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AllureAppContext>(option =>
            {
                option.UseSqlServer(config.GetConnectionString("Db_Connection"));
            });

            services.AddScoped<DbContext, AllureAppContext>();

            // Entities
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<AdminNavItem>, Repository<AdminNavItem>>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IRepository<AdminRole>, Repository<AdminRole>>();




            // Repo
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();


            //Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RolesService>();

        }
    }
}
