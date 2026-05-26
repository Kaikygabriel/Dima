using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Api.EndPoints.Categories;

public class UpdateCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/v1",async
                (UpdateCategoryRequest request,ICategoryHandler handler) =>
            {
                var result = await handler.Update(request);
                return
                    result.IsSuccess ? 
                        Results.Ok(result.Data) :
                        Results.BadRequest(result.Error);
            })
            .WithName("Categories : update")
            .WithSummary("Update Category")
            .Produces<Response<Category>>();

    }
}