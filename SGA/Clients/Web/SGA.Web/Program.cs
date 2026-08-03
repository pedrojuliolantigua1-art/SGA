using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SGA.Web;
using SGA.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Un único HttpClient para toda la app (sin IHttpClientFactory), apuntando a SGA.Api.
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7168/"),
    Timeout = TimeSpan.FromSeconds(30)
});

builder.Services.AddScoped<SgaApiService>();
builder.Services.AddScoped<SesionState>();

await builder.Build().RunAsync();
