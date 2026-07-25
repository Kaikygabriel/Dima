namespace Dima.Core.Requests.Orders;

public record RefundOrderRequest : Request
{
    public Guid Id { get; set; }
};