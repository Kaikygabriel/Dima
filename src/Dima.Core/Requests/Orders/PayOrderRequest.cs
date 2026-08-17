using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Orders;

public record PayOrderRequest : Request
{
    [Required]
    public Guid Id { get; set; }
} 