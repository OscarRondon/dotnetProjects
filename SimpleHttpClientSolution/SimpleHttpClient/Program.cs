using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SimpleHttpClient.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Custon configuration File
builder.Configuration.AddJsonFile("env.json", optional: true, reloadOnChange: true);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient("RapidApi", opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration.GetSection("RapidApi:WeatherApi:BaseUrl").Value);
    opt.DefaultRequestHeaders.Add("X-RapidAPI-Key", builder.Configuration.GetSection("RapidApi:WeatherApi:X-RapidApi-Key").Value);
    opt.DefaultRequestHeaders.Add("X-RapidAPI-Host", builder.Configuration.GetSection("RapidApi:WeatherApi:X-RapidAPI-Host").Value);
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
