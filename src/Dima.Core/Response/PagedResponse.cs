using System.Text.Json.Serialization;
using Dima.Core.Configurations;

namespace Dima.Core.Response;

public class PagedResponse<T> : Response<T>
{
    [JsonConstructor]
    public PagedResponse()
    {
        
    }
    public PagedResponse(T data,int currentPage,int totalCount,int pageSize = Configuration.DefaultPageSize) : base(data)
    {
        CurrentPage = currentPage;
        TotalCount = totalCount;
        PageSize = pageSize;
    }

    public PagedResponse(Error error) : base(error)
    {
        
    }
    public int CurrentPage { get; set; }
    public int PageTotal => (int)Math.Ceiling((double)TotalCount / PageSize);
    public int TotalCount { get; set; }
    public int PageSize { get; set; } 
    
    public static implicit operator PagedResponse<T>(Error error)
        => new (error);

}