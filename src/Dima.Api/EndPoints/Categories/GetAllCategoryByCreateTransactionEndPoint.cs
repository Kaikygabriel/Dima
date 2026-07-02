using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Categories;

public class GetAllCategoryByCreateTransactionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/v1/{userId:guid}", async
        (
            [FromRoute]Guid userId,
            CancellationToken cancellationToken, 
            [FromServices] ICategoryHandler handler) =>
        {
            var result = await handler.GetAllCategoryToCreateTransaction(userId, cancellationToken);

            return Results.Ok(result);
        }).RequireAuthorization();
    }
}