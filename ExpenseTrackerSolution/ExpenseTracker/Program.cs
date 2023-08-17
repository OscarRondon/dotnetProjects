using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ExpenseTracker.Configuration;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace ExpenseTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Custon configuration File
            builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<EnvConfigurations>();
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => 
                {
                    options.UseSqlServer(DbConnection(builder.Configuration));
                }
            );
            var app = builder.Build();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(app.Configuration.GetSection("SyncfusionLicense").Value);

            //EnvConfigurations _envConfigurationService = app.Services.GetService<EnvConfigurations>();

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
                pattern: "{controller=Dashboard}/{action=Index}/{id?}"
            );

            app.Run();
        }

        private static string DbConnection(ConfigurationManager configuration)
        {
            string dbServer = configuration.GetSection("DataBase:DBServer").Value ?? "";
            string dbName = configuration.GetSection("DataBase:DBName").Value ?? "";
            string dbUser = configuration.GetSection("DataBase:DBUser").Value ?? "";
            string dbPwd = configuration.GetSection("DataBase:DBPwd").Value ?? "";
            string dbConnectioString = configuration.GetSection("DataBase:ConnectionStrings:MSSql").Value ?? "";
            dbConnectioString = dbConnectioString.Replace("{DBServer}", dbServer);
            dbConnectioString = dbConnectioString.Replace("{DBName}", dbName);
            dbConnectioString = dbConnectioString.Replace("{DBUser}", dbUser);
            dbConnectioString = dbConnectioString.Replace("{DBPwd}", dbPwd);
            
            return dbConnectioString;
        }
    }
}