namespace Dima.Core.Requests.Orders;

public record GetVoucherByCodeRequest : Request
{
    public string Code { get; set; }
}