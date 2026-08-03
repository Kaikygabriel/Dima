using System.Security.Claims;
using Dima.Api.Interfaces.Endpoint;
using Dima.Core.Handler;
using Dima.Core.Requests.Orders;

namespace Dima.Api.EndPoints.Voucher;

public class GetVoucherByCodeEndPoint  : IEndPoint
{
    public static void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{code}",
            async (string code, IVoucherHandler handler, 
                ClaimsPrincipal user) =>
            {
                var request = new GetVoucherByCodeRequest()
                {
                    Code = code,
                    UserId = Guid.Parse(user.Identity?.Name ?? Guid.Empty.ToString())
                };
                var result = await handler.GetByCodeAsync(request);
                return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
            });
    }
}