using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentApp.Core.DB_Context;
using StudentApp.Repository.Implementation;
using StudentApp.Repository.Interface;

namespace StudentApp.Repository
{
    public static class ConfigureServices
    {
        public static void RegisterServices(IServiceCollection  services, IConfiguration config)
        {
            services.AddDbContext<StudentAppContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Db_Connection"));
            });

            // Context
            services.AddScoped<DbContext, StudentAppContext>();

            // Repo
            services.AddScoped<IStudentRepo, StudentRepo>();
        }
    }
}
