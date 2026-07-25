namespace Dima.Core.Requests.Orders;

public record CreateOrderRequest : Request
{
    public Guid ProductId { get; set; }
    public Guid? VoucherId { get; set; }
    
};