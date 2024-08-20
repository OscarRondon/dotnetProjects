global using PortfolioShared;
global using PortafolioClient.Services.ProjectService;
global using PortafolioClient.Services.MailService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortafolioClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IMailService, MailService>();

#if DEBUG
    await Task.Delay(5000);
#endif

await builder.Build().RunAsync();
