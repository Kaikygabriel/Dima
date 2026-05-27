using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Transactions;

public class UpdateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/v1", async 
            ( [FromBody]UpdateTransactionRequest request,[FromServices] ITransactionHandler handler,CancellationToken can) =>
        {
            var result = await handler.Update(request,can);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        });
    }
}