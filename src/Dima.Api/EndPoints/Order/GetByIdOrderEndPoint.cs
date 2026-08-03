using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;

namespace Dima.Api.EndPoints.Order;

public class GetByIdOrderEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{id:guid}", async 
            (IOrderHandler handler,Guid id,ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.Identity?.Name ?? Guid.Empty.ToString());
            var request = new GetOrderByIdRequest()
            {
                Id = id,
                UserId = userId
            };
            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
        });
    }
}