using Dima.Api.Data.Context;
using Dima.Api.EndPoints;
using Dima.Api.Handlers;
using Dima.Core.Handler;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                 throw new Exception("Connection String Not Found !");

builder.Services.AddLogging();

builder.WebHost.ConfigureKestrel(x => x.AddServerHeader = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(x => 
    x.UseSqlServer(connection));
    
builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();

builder.Services.AddExceptionHandler<ExceptionGlobalHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.MapOpenApi();

app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","Dima v1"));

app.MapEndpoints();

app.Run();