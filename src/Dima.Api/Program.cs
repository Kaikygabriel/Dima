using Dima.Api.EndPoints;
using Dima.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDependency();
builder.AddConfigurationLogging();
builder.AddSecurity();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","Dima v1"));
    app.UseExceptionHandler();
}

app.MapEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.Run();