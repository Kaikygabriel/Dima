using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Order;

public class GetAllOrdersEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("",async ([FromQuery] int pageSize,[FromQuery] int page,ClaimsPrincipal claims,[FromServices] IOrderHandler orderHandler) =>
        {
            var request = new GetAllOrdersRequest()
            {
                Page = page,
                PageSize = pageSize,
                UserId = Guid.Parse(claims.Identity?.Name ?? Guid.Empty.ToString())
            };
            var result = await orderHandler.GetAllAsync(request);
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
            
        });
    }
}