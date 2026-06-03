using Dima.Api.EndPoints;
using Dima.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDependency();
builder.AddConfigurationLogging();
builder.AddSecurity();
builder.AddCors();
builder.Services.AddDocumentation();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.UseDeveloperEnvironment();

app.MapEndpoints();

app.UseCors();

app.UseSecurity();

app.Run();