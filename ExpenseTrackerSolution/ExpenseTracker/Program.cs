using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ExpenseTracker.Configuration;

namespace ExpenseTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<EnvConfigurationService>();

            // DI
            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer();
                }
            );

            var app = builder.Build();

            EnvConfigurationService _envConfigurationService = app.Services.GetService<EnvConfigurationService>();
            EnvConfiguration _envConfiguration = _envConfigurationService.Config;

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}