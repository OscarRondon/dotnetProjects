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

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddIdentity<IdentityUser,  IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.ConfigureApplicationCookie(conf =>
{
    conf.LoginPath = "/Identity/Account/Login";
});

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

#region Seeding
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User", "Financial", "Logistics" };
    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string adminEmail = "admin@admin.com";
    string adminPass = "Admin.123";
    

    if(await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var userAdmin = new IdentityUser
        {
            UserName = "Admin",
            Email = adminEmail
        };

        await userManager.CreateAsync(userAdmin, adminPass);

        await userManager.AddToRoleAsync(userAdmin, "Admin");
    }
}
#endregion

app.Run();
