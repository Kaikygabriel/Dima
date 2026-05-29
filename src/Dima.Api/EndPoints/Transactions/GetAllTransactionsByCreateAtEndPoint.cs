using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Transactions;

public class GetAllTransactionsByCreateAtEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("v1/ByCreate/{userId:guid}/{page:int}/{pageSize}", async (
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
            var response = await handler.GetAllByCreateAt(request,cl);

            return response.IsSuccess ? 
                Results.Ok(response) :
                Results.BadRequest(response);
        });
    }
}