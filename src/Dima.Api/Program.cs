using Dima.Api.Category.Handler;
using Dima.Api.Category.Request;
using Dima.Api.Category.Response;
using Dima.Api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(x => x.AddServerHeader = false);

builder.Services.AddTransient<IHandler<CreateCategoryRequest,CreateCategoryResponse>,CreateCategoryHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseSwaggerUI(x=>x.SwaggerEndpoint("/openapi/v1.json","Dima v1"));

app.MapPost("/v1/Categories",async
        (CreateCategoryRequest request,IHandler<CreateCategoryRequest,CreateCategoryResponse> handler) =>
{
    var result = await handler.Handle(request);
    return Results.Ok(result);
})
    .WithName("Categories  : create")
    .WithSummary("Create new Category")
    .Produces<CreateCategoryResponse>();

app.Run();