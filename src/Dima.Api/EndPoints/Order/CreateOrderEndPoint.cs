using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Order;

public class CreateOrderEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("",async ([FromBody] CreateOrderRequest request,ClaimsPrincipal claims,[FromServices] IOrderHandler orderHandler) =>
        {
            if (claims.Identity?.Name != request.UserId.ToString())
                return Results.Unauthorized();
            var result = await orderHandler.CreateAsync(request);
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
            
        });
    }
}