using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;

namespace Dima.Api.EndPoints.Products;

public class GetProductByIdEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{id:guid}",
                async ( IProductHandler handler,Guid id,ClaimsPrincipal user) =>
            {
                var request = new GetProductByIdRequest()
                {
                    Id = id,
                    UserId = Guid.Parse(user.Identity?.Name ?? Guid.Empty.ToString())
                };
                var result = await handler.GetByIdAsync(  request);
            
                return result.IsSuccess ?
                    Results.Ok(result) :
                    Results.BadRequest(result);
            });
    }
}