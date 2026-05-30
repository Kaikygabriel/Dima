namespace Dima.Core.Requests.Transaction;

public record GetTransactionsRequest(DateTime? Start,DateTime? End) : PagedRequest;