using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Products;

public class GetAllProductsEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", async
            (IProductHandler handler, [FromQuery] int page,[FromQuery] int pageSize,ClaimsPrincipal user ) =>
        {
            var request = new GetAllProductsRequest()
            {
                Page = page,
                PageSize = pageSize,
                UserId = Guid.Parse(user.Identity?.Name ?? Guid.Empty.ToString())
            };
            var result = await handler.GetAllAsync(request);
            
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
        });
    }
}