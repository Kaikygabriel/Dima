using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;

namespace Dima.Api.EndPoints.Identity;

public class GetRolesEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("Identity/Roles", (ClaimsPrincipal user) =>
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return Results.Unauthorized();
        
            var roles = user.Claims
                .Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)
                .Select(x =>
                    new
                    {
                        x.Issuer,
                        x.Type,
                        x.Value,
                        x.ValueType,
                        x.OriginalIssuer
                    }
                );
        
            return Results.Ok(Dima.Core.Response.Response<IEnumerable<object>>.Success(roles));
        }).RequireAuthorization();
    }
}