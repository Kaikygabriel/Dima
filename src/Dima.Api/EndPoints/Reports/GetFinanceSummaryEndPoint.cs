using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Reports;

public class GetFinanceSummaryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/category/finance/{userId:guid}", 
            async ([FromRoute]Guid userId,[FromServices] IReportHandler handler) =>
            {
                var result = await handler.GetFinanceSummaryAsync(userId);
                return result.IsSuccess ?
                    Results.Ok(result):
                    Results.BadRequest(result);
            });
    }
}