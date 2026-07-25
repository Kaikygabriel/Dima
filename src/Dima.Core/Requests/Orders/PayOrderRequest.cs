namespace Dima.Core.Requests.Orders;

public record PayOrderRequest : Request
{
    public Guid Id { get; set; }
} ;