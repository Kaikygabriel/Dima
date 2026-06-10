using Dima.Api.Interfaces.Endpoint;

namespace Dima.Api.EndPoints.Identity;

public class LogoutEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/Logout",async (HttpContext context) =>
        {
            var authenticationKey = ".AspNetCore.Identity.Application";
            context.Response.Cookies.Delete(authenticationKey);
            return Results.Ok(Dima.Core.Response.Response<string>.Success());
        }).RequireAuthorization();

    }
}