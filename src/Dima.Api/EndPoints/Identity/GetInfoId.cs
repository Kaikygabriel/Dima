using System.Security.Claims;
using Dima.Api.Data.Context;
using Dima.Api.Interfaces.Endpoint;

namespace Dima.Api.EndPoints.Identity;

public class GetInfoId : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/manage/Id", (ClaimsPrincipal claims, AppDbContext context) =>
        {
            if (claims.Identity is null || !claims.Identity.IsAuthenticated)
                return Results.Unauthorized();
            var id = Guid.Parse(claims.Identity.Name!);

            return Results.Ok(context.Users.FirstOrDefault(x => x.Id == id)!.Id);
        });     
    }
}