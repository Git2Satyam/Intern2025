using EcommApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EcommApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                     .AddCookie(options =>
                     {
                         options.LoginPath = "/Auth/Authentication/Index"; 
                         options.LogoutPath = "/Auth/Authentication/Logout"; 
                         options.AccessDeniedPath = "/Auth/Authentication/Index";
                         options.SlidingExpiration = false; 
                         options.Cookie.Name = "PizzaHubAuth";
                         options.Cookie.HttpOnly = true;
                         options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                         options.Cookie.SameSite = SameSiteMode.Lax;
                     });

            ConfigureServices.RegisterServices(builder.Services, builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
             name: "Auth",
             pattern: "{area:exists}/{controller=Authentication}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=GetProducts}/{id?}");

            app.Run();
        }
    }
}
