using eCommerceTickets.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Custon configuration File
builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(GetDbStringConnection(builder.Configuration));
});
builder.Services.AddControllersWithViews();

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

app.Run();


static string GetDbStringConnection(ConfigurationManager Configuration)
{
    // Get values from .env file
    string dbServer = Configuration.GetSection("DataBase:DBServer").Value ?? "";
    string dbName = Configuration.GetSection("DataBase:DBName").Value ?? "";
    string dbUser = Configuration.GetSection("DataBase:DBUser").Value ?? "";
    string dbPwd = Configuration.GetSection("DataBase:DBPwd").Value ?? "";
    string dbConnectioString = Configuration.GetSection("DataBase:ConnectionStrings:MSSql").Value ?? "";
    dbConnectioString = dbConnectioString.Replace("{DBServer}", dbServer);
    dbConnectioString = dbConnectioString.Replace("{DBName}", dbName);
    dbConnectioString = dbConnectioString.Replace("{DBUser}", dbUser);
    dbConnectioString = dbConnectioString.Replace("{DBPwd}", dbPwd);

    var connectionString = dbConnectioString ?? throw new InvalidOperationException("Connection string not found.");

    return connectionString;
}