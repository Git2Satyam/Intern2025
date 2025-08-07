using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaHub.Core.DB_Context;
using PizzaHub.Repository.Implementation;
using PizzaHub.Repository.Interface;
using PizzaHub.Services.Implementation;
using PizzaHub.Services.Interface;

namespace EcommApp.Services
{
    public static class ConfigureServices
    {
        public static void RegisterServices(IServiceCollection _services, IConfiguration _config)
        {
            _services.AddDbContext<PizzaHubContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("Db_Connection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            // Context
            _services.AddScoped<DbContext, PizzaHubContext>();

            // Repo
            _services.AddScoped<IProductRepo, ProductRepo>();
            _services.AddScoped<ICartRepo, CartRepo>();


            // Services
            _services.AddScoped<IProductService, ProductService>();
            _services.AddScoped<ICartService, CartService>();


        }
    }
}
