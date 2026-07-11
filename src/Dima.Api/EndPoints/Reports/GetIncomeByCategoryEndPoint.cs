using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Reports;

public class GetIncomeByCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/category/incomes/{userId:guid}", 
            async ([FromRoute]Guid userId,[FromServices] IReportHandler handler) =>
        {
            var result = await handler.GetIncomeByCategoryAsync(userId);
            return result.IsSuccess ?
                    Results.Ok(result):
                    Results.BadRequest(result);
        });
    }
}