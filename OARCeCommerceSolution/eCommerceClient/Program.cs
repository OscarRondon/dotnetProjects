global using eCommerceShared;
global using eCommerceClient.Settings;
global using eCommerceClient.Services.ProductService;
global using eCommerceClient.Services.CategoryService;
using eCommerceClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var settings = new ClientAppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
