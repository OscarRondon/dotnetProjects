using ExternalOAuth2.Server.Data;
using ExternalOAuth2.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration.GetSection("GoogleAuth:ClientId").Value;
        googleOptions.ClientSecret = builder.Configuration.GetSection("GoogleAuth:ClientSecret").Value;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
