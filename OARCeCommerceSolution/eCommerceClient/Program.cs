global using eCommerceShared;
global using eCommerceClient.Settings;
global using eCommerceClient.Services.ProductService;
global using eCommerceClient.Services.ProductTypeService;
global using eCommerceClient.Services.CategoryService;
global using eCommerceClient.Services.CartService;
global using eCommerceClient.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using eCommerceClient.Services.OrderService;
global using eCommerceClient.Services.AddressService;
global using MudBlazor.Services;
using eCommerceClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var settings = new ClientAppSettings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();
