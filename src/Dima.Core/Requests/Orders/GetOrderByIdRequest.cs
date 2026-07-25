namespace Dima.Core.Requests.Orders;

public record GetOrderByIdRequest : Request
{
    public Guid Id { get; set; }
};