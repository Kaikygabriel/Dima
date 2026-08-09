using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Order;

public class PayOrderEndPoint  : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/pay",async ([FromBody] PayOrderRequest request,ClaimsPrincipal claims,[FromServices] IOrderHandler orderHandler) =>
        {
            request.UserId = Guid.Parse(claims.Identity?.Name ?? Guid.Empty.ToString() );
            
            var result = await orderHandler.PayAsync(request);
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
        });
    }
}