using AuthenticationAuthorizacion.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AuthDbContext>(opt =>
{
    opt.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<IdentityUser,  IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
