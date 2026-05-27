using Dima.Core.Enum;

namespace Dima.Core.Requests.Transaction;

public record CreateTransactionRequest(string Title,ETypeTransaction Type,decimal Amount,Guid CategoryId,DateTime? PaidOrReceivedAt)
    : Request;