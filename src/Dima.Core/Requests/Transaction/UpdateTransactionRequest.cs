using Dima.Core.Enum;

namespace Dima.Core.Requests.Transaction;

public record UpdateTransactionRequest
    : Request
{
    public UpdateTransactionRequest()
    {
        
    }
    public Guid Id { get; init; } 
    public string Title { get; set; }
    public ETypeTransaction Type { get; set; } 
    public decimal Amount { get; set; } 
    public Guid CategoryId { get; set; }
    public DateTime? PaidOrReceivedAt { get; set; }

    public static implicit operator UpdateTransactionRequest(Models.Transaction transaction)
        => new ()
        {
            Id = transaction.Id,
            Title = transaction.Title,
            Type = transaction.EType,
            Amount = transaction.Amount,
            CategoryId = transaction.CategoryId,
            PaidOrReceivedAt = null
        };
}