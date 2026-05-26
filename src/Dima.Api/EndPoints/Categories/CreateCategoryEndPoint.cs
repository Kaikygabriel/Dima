using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Api.EndPoints.Categories;

public class CreateCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/v1",async
                (CreateCategoryRequest request,ICategoryHandler handler) =>
            {
                var result = await handler.Create(request);
                return
                    result.IsSuccess ? 
                        Results.Created() :
                        Results.BadRequest(result.Error);
            })
            .WithName("Categories : create")
            .WithSummary("Create new Category")
            .Produces<Response<Category>>();
    }
}