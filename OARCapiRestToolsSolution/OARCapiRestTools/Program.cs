global using MimeKit;
global using MimeKit.Text;
global using MailKit.Net.Smtp;
global using MailKit.Security;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OARC Api Rest Tools",
        Description = "restApi for common use for OARC developments",
        Contact = new OpenApiContact
        {
            Name = "Oscar Rondon",
            Email = "oscar.rondon.c@treeoflifesoftware.dev",
            Url = new Uri("https://github.com/OscarRondon")
        },
        License = new OpenApiLicense
        {
            Name = "MIT Licence",
            Url = new Uri("https://opensource.org/licenses/MIT")
        },
        Version = "v1"
    });

    //Generate the xml docs tha'll drive the swagger docs
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);

    opt.CustomOperationIds(apiDesc =>
    {
        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
    });
}
);
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static string[] GetCORS(ConfigurationManager Configuration)
{
    // Get values from .env file
    string[] cors = Configuration.GetSection("AllowedHosts").Get<string[]>().Length > 0 ? Configuration.GetSection("AllowedHosts").Get<string[]>() : ["*"];
    return cors;
}