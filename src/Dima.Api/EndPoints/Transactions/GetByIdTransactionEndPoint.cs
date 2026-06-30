using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Transaction;

namespace Dima.Api.EndPoints.Transactions;
 
public class GetByIdTransactionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("v1/{id:guid}/{userId:guid}", async (Guid id, Guid userId, ITransactionHandler handler,CancellationToken cl) =>
        {
            var request = new GetTransactionsByIdRequest(id) { UserId = userId };
            var response = await handler.GetById(request,cl);

            return response.IsSuccess ? 
                Results.Ok(response) :
                Results.BadRequest(response);
        }).WithName("GetTransaction");
    }
}