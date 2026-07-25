namespace Dima.Core.Requests.Orders;

public record CancelOrderRequest : Request
{
    public Guid Id { get; set; }
};