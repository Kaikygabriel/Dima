using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Category;
using Dima.Core.Response;

namespace Dima.Api.EndPoints.Categories;

public class DeleteCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/v1/{id:guid}/{userId:guid}",async
                (Guid id,Guid userId,ICategoryHandler handler) =>
            {
                var request = new DeleteCategoryRequest(id,userId);
                var result = await handler.Delete(request);
                return
                    result.IsSuccess ? 
                        Results.Ok(result.Data) :
                        Results.BadRequest(result.Error);
            })
            .WithName("Categories : delete")
            .WithSummary("Delete Category")
            .Produces<Response<Category>>();

    }
}