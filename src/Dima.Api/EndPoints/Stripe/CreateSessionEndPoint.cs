using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Stripe;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.EndPoints.Stripe;

public class CreateSessionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("",async ([FromBody] CreateSessionRequest request,ClaimsPrincipal claims,[FromServices] IStripeHandler stripeHandler) =>
        {
            request.UserId = Guid.Parse(claims.Identity?.Name ?? Guid.Empty.ToString() );
            
            var result = await stripeHandler.CreateSession(request);
            return result.IsSuccess ?
                Results.Ok(result) :
                Results.BadRequest(result);
        });
    }
}