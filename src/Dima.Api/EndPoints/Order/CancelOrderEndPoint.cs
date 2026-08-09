using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;

namespace Dima.Api.EndPoints.Order;

public class CancelOrderEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPut("/cancel",
            async (IOrderHandler handler,CancelOrderRequest request,ClaimsPrincipal claims,CancellationToken cancellationToken) =>
            {
                request.UserId = Guid.Parse(claims.Identity?.Name ?? Guid.Empty.ToString() );
                
                var result = await handler.CancelAsync(request, cancellationToken);
                return result.IsSuccess ?
                    Results.Ok(result) :
                    Results.BadRequest(result);
            });
    }
}