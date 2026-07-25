namespace Dima.Core.Requests.Orders;

public record GetProductByIdRequest : Request
{
    public Guid Id { get; set; }
};