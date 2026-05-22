using Dima.Api.Data.Context;
using Dima.Api.Extensions;
using Dima.Api.Handlers;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                 throw new Exception("Connection String Not Found !");

builder.WebHost.ConfigureKestrel(x => x.AddServerHeader = false);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(x => 
    x.UseSqlServer(connection));
    
builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseGlobalExceptionHandlerDevelopment();
}

app.MapOpenApi();

app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","Dima v1"));

app.MapPost("/v1/Categories",async
        (CreateCategoryRequest request,ICategoryHandler handler) =>
{
    var result = await handler.Create(request);
    return Results.Ok(result);
})
    .WithName("Categories  : create")
    .WithSummary("Create new Category")
    .Produces<Response<Category>>();

app.MapGet("/v1/Categories",async
         (ICategoryHandler handler) =>
    {
        var result = await handler.GetAll(new GetAllCategoryRequest());
        return Results.Ok(result);
    })
    .WithName("Categories  : get all")
    .WithSummary("Get all Categories")
    .Produces<Response<List<Category>>>();

app.Run();