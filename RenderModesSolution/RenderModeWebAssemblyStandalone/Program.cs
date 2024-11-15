using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RenderModeWebAssemblyStandalone;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

await builder.Build().RunAsync();
