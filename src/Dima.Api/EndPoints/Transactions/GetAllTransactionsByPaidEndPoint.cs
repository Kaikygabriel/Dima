using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Transactions;

public class GetAllTransactionsByPaidEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("v1/ByPaid/{userId:guid}/{page:int}/{pageSize}", async (
            [FromQuery]DateTime? start,
            [FromQuery]DateTime? end,
            int page ,
            int pageSize ,
            Guid userId,
            ITransactionHandler handler,
            CancellationToken cl) =>
        {
            var request = new GetTransactionsRequest(start,end)
            {
                Page = page,
                PageSize = pageSize,
                UserId = userId
            };
            Console.WriteLine(request.UserId);
            var response = await handler.GetAllByPaidOrReceivedAt(request,cl);

            return response.IsSuccess ? 
                Results.Ok(response) :
                Results.BadRequest(response);
        });
    }
}