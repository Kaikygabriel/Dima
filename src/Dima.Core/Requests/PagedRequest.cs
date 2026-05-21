using Dima.Core.Configurations;

namespace Dima.Core.Requests;

public abstract record PagedRequest : Request
{
    public int Page { get; set; } = 0;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
}