using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Reports;

public class GetExpensesByCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/category/expenses/{userId:guid}", 
            async ([FromRoute]Guid userId,[FromServices] IReportHandler handler) =>
            {
                var result = await handler.GetExpensesByCategoryAsync(userId);
                return result.IsSuccess ?
                    Results.Ok(result):
                    Results.BadRequest(result);
            });
    }
}