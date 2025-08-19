using FoodApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodApp.UI
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
                                options.LoginPath = "/Auth/Authentication/LoginForm";
                                options.AccessDeniedPath = "/Auth/Authentication/LoginForm";
                                options.Cookie.Name = "FoodAppAuth";
                                options.SlidingExpiration = true;
                                options.Cookie.HttpOnly = true;
                                options.Cookie.IsEssential = true;
                                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                                options.Cookie.SameSite = SameSiteMode.Lax;
                            });

            ConfigureServices.RegisterService(builder.Services, builder.Configuration);

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Authentication}/{action=LoginForm}/{id?}"
                );
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
