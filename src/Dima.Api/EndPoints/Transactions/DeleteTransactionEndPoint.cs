using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Transactions;

public sealed class DeleteTransactionEndPoint : IEndPoint 
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/v1/{id:guid}/{userId:guid}", async 
            ( Guid id, Guid userId,[FromServices] ITransactionHandler handler,CancellationToken can) =>
        {
            var request = new DeleteTransactionRequest(id) { UserId = userId };
            var result = await handler.Delete(request,can);
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        });
    }
}