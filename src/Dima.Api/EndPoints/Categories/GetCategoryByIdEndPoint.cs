using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Api.EndPoints.Categories;

public class GetCategoryByIdEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/v1/{id}/{userId}", async
                ( ICategoryHandler handler, Guid id, Guid userId) =>
            {
                var request = new GetCategoryByIdRequest(id) { UserId = userId };

                var result = await handler.GetById(request);

                return result.IsSuccess ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("Categories : get by id")
            .WithSummary("Get category by id");
        
    }
}