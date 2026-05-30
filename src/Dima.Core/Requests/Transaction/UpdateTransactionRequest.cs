using System.ComponentModel.DataAnnotations;
using Dima.Core.Enum;

namespace Dima.Core.Requests.Transaction;

public record UpdateTransactionRequest(
    [Required]Guid Id,
    [Required]string Title,
    [Required]ETypeTransaction Type,
    [Required]decimal Amount,
    [Required]Guid CategoryId,
    [Required]DateTime? PaidOrReceivedAt)  
    : Request;