using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Stripe;

public record GetTransactionsByOrderRequest : Request
{
    [Required]
        public string Id { get; set; } = null!;
}