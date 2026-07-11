using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Reports;

public class GetExpensesAndIncomeEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/category/incomes/expenses/{userId:guid}", 
            async ([FromRoute]Guid userId,
                   [FromQuery] int? month,
                   [FromQuery] int? year,
                   [FromServices] IReportHandler handler) =>
            {
                if(month is null)
                    month = DateTime.UtcNow.Month;
                if(year is null)
                    year = DateTime.UtcNow.Month;
                
                var result = await handler.GetIncomeAndExpensesAsync(userId,(int)month,(int)year);
                return result.IsSuccess ?
                    Results.Ok(result):
                    Results.BadRequest(result);
            });
    }
}