using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Areas.Identity.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add Custon configuration File
builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);

// Get values from .env file
string dbServer = builder.Configuration.GetSection("DataBase:DBServer").Value ?? "";
string dbName = builder.Configuration.GetSection("DataBase:DBName").Value ?? "";
string dbUser = builder.Configuration.GetSection("DataBase:DBUser").Value ?? "";
string dbPwd = builder.Configuration.GetSection("DataBase:DBPwd").Value ?? "";
string dbConnectioString = builder.Configuration.GetSection("DataBase:ConnectionStrings:MSSql").Value ?? "";
dbConnectioString = dbConnectioString.Replace("{DBServer}", dbServer);
dbConnectioString = dbConnectioString.Replace("{DBName}", dbName);
dbConnectioString = dbConnectioString.Replace("{DBUser}", dbUser);
dbConnectioString = dbConnectioString.Replace("{DBPwd}", dbPwd);

var connectionString = dbConnectioString ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AuthDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
