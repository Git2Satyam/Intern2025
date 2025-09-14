
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.DB_Context;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                       .AddJsonOptions(options =>
                       {
                           options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // For deserialization
                           options.JsonSerializerOptions.PropertyNamingPolicy = null;
                       });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StudentContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(cor => cor.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.MapControllers();

            app.Run();
        }
    }
}
