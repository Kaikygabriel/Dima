using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Transactions;

public class CreateTransactionEndpoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/v1", async 
            (CreateTransactionRequest request ,[FromServices] ITransactionHandler handler,CancellationToken can) =>
        {
            Console.WriteLine(request.UserId);
                var result = await handler.Create(request,can);
                
                return result.IsSuccess ? Results.CreatedAtRoute(
                "GetTransaction",
                new
                {
                    id = result.Data!.Id,
                    userId = result.Data!.UserId
                }) : Results.BadRequest(result);
        });
    }
}