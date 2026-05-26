using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Api.EndPoints.Categories;

public class GetAllCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/v1/{userId:guid}/{page}/{pageSize}",async
                (ICategoryHandler handler,Guid userId, int page,int pageSize) =>
            {
                var request = new GetAllCategoryRequest
                {
                    UserId = userId,
                    Page = page,
                    PageSize = pageSize
                };
                var result = await handler.GetAll(request);
        
                return result.IsSuccess ?
                    Results.Ok(result) :
                    Results.BadRequest(result);
            })
            .WithName("Categories : get all")
            .WithSummary("Get all Categories")
            .Produces<PagedResponse<List<Category>>>();
    }
}