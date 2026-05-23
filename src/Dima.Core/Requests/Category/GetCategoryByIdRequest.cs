using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Dima.Core.Requests.Category;

public sealed record GetCategoryByIdRequest(Guid Id) : Request
{
    public static GetCategoryByIdRequest Parse(string s, IFormatProvider? provider)
    {
        var result = JsonSerializer.Deserialize<GetCategoryByIdRequest>(s);
        
        return result ;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out GetCategoryByIdRequest result)
    {
        try
        {
            result = JsonSerializer.Deserialize<GetCategoryByIdRequest>(s);
            return true;
        }
        catch(Exception e)
        {
            result = null;
            return false;
        }
    }
}