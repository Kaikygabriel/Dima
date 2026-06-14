using Dima.Core.Handler;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Pwa;
using Dima.Pwa.Handlers;
using Dima.Pwa.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.ApiUrlHttps = builder.Configuration["BackEndUrl"] ?? throw new Exception("Not Found Url Of BackEnd!");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddScoped<ICookieAuthenticationStateProvider, CookieAuthenticationProvider>();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider )x.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddScoped<IUserHandler,UserHandler>();
builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, x =>
{
    x.BaseAddress = new Uri(Configuration.ApiUrlHttps);
}).AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();