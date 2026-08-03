using System.Diagnostics.CodeAnalysis;

namespace Dima.Core.Requests.Orders;

public record GetAllOrdersRequest : PagedRequest,IParsable<GetAllOrdersRequest>
{
    public static GetAllOrdersRequest Parse(string s, IFormatProvider? provider)
    {
        var value = s.Split('?')[1]
            .Split('&')
            .Select(x=>x.Split('='))
            .ToDictionary(
                x=> x[0],
                x=> x[1]
            );
        var result = new GetAllOrdersRequest()
        {
            UserId = Guid.Parse(value["userId"]),
            Page = int.Parse(value["page"]),
            PageSize = int.Parse(value["pageSize"])
        };
        return result;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GetAllOrdersRequest result)
    {
        try
        {
            var value = s?.Split('?')[1]
                .Split('&')
                .Select(x=>x.Split('='))
                .ToDictionary(
                    x=> x[0],
                    x=> x[1]
                );
            result = new GetAllOrdersRequest()
            {
                UserId = Guid.Parse(value["userId"]),
                Page = int.Parse(value["page"]),
                PageSize = int.Parse(value["pageSize"])
            };
            return true;
        }
        catch (Exception e)
        {
            result = null;
            return false;
        }
    }
}