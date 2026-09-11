using Dima.Api.Configurations;
using Dima.Api.EndPoints;
using Dima.Api.Extensions;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDependency();
builder.AddConfigurationLogging();
builder.AddSecurity();
builder.AddCors();
builder.Services.AddDocumentation(builder.Configuration);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<
    IUserClaimsPrincipalFactory<User>,
    CustomClaimsPrincipalFactory>();
builder.Services.AddScoped<StorageConfiguration>(x => new StorageConfiguration()
{
    ConnectionString  = builder.Configuration["Storage:ConnectionString"] ?? throw new Exception("Not Found Connection string Storage"),
    ContainerImage =  builder.Configuration["Storage:ContainerImage"] ?? throw new Exception("Not Found Container name Of Storage"),
});

builder.WebHost.UseKestrel(x=>x.AddServerHeader = false);

var app = builder.Build();

app.UseDeveloperEnvironment();

app.MapEndpoints();

app.UseStaticFiles();

app.UseCors();

app.UseSecurity();

app.Run();