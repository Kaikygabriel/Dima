using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Order;

public class RefoundOrderEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/refund",async ([FromBody]RefundOrderRequest  request,ClaimsPrincipal claims,[FromServices] IOrderHandler orderHandler) =>
        {
            request.UserId = Guid.Parse(claims.Identity?.Name ?? Guid.Empty.ToString() );
            
            var result = await orderHandler.RefundAsync(request);
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
        });
    }
}