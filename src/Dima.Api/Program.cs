using Dima.Api.Data.Context;
using Dima.Api.Handlers;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                 throw new Exception("Connection String Not Found !");
Console.WriteLine(Fatorial(5));
builder.WebHost.ConfigureKestrel(x => x.AddServerHeader = false);

builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(x => 
    x.UseSqlServer(connection));
    
var app = builder.Build();

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

app.Run();

int Fatorial(int count ,int num = 1)
{
    if (count <= 0)
        return num;
    
    num *= count ;
    return Fatorial(count- 1,num);
}