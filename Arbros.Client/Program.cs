using Arbros.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSyncfusionBlazor();

// Cambiar la línea de configuración del HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7119/") });

await builder.Build().RunAsync();
