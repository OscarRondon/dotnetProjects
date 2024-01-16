global using Microsoft.EntityFrameworkCore;
global using eCommerceShared;
global using eCommerceServer.Data;
global using eCommerceServer.Services.ProductService;
global using eCommerceServer.Services.CategoryService;
global using eCommerceServer.Services.CartService;
global using eCommerceServer.Services.AuthService;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("env.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(GetDbStringConnection(builder.Configuration));
    //opt.UseSqlServer(GetAZDbStringConnection(builder.Configuration));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        configurePolicy =>
        {
            configurePolicy.WithOrigins(GetCORS(builder.Configuration))
            .AllowAnyHeader().AllowAnyMethod();
        }
        );
});
// Customs services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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

static string[] GetCORS(ConfigurationManager Configuration)
{
    // Get values from .env file
    string[] cors = Configuration.GetSection("CORS:AllowOrigin").Get<string[]>().Length > 0 ? Configuration.GetSection("CORS:AllowOrigin").Get<string[]>() : ["*"];
    return cors;
}