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
builder.Services.AddDocumentation();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddScoped<
    IUserClaimsPrincipalFactory<User>,
    CustomClaimsPrincipalFactory>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperEnvironment();

app.MapEndpoints();

app.UseCors();

app.UseSecurity();

app.Run();