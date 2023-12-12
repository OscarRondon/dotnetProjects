global using eCommerceShared;
global using eCommerceClient.Services.AppService;
global using eCommerceClient.Services.ProductService;
using eCommerceClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();
