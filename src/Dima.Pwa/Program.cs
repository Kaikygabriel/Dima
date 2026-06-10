using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Pwa;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddHttpClient(Configuration.HttpClientName, x =>
{
    x.BaseAddress = new Uri(Configuration.ApiUrlHttps);
}).AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();