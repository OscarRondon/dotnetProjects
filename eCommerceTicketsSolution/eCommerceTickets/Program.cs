using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
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
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddScoped<IProducersService, ProducersService>();
builder.Services.AddScoped<ICinemasService, CinemasServices>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddScoped(sc => ShoppingCartService.GetShoppingCart(sc));
builder.Services.AddSession();

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

app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Errors/NotFound";
        await next();
    }
});

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

//Seed Database
AppDbInitializer.Seed(app);

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