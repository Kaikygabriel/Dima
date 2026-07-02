using System.ComponentModel.DataAnnotations;
using Dima.Core.Enum;

namespace Dima.Core.Requests.Transaction;

public record CreateTransactionRequest
    : Request
{
    [Required]
    public string Title { get; set; }= null!;
    [Required]
    public ETypeTransaction Type { get; set; } = ETypeTransaction.Out;
    [Required]
    public decimal Amount { get; set; } 
    [Required]
    public Guid CategoryId { get; set; }
    public DateTime? PaidOrReceivedAt { get; set; } 
}