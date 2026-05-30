using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface ITransactionHandler
{
    Task<PagedResponse<IEnumerable<Transaction>>> GetAllByCreateAt(GetTransactionsRequest request,
        CancellationToken cancellationToken = default);
    Task<PagedResponse<IEnumerable<Transaction>>> GetAllByPaidOrReceivedAt(GetTransactionsRequest request,CancellationToken cancellationToken = default);
    Task<Response<Transaction>> GetById(GetTransactionsByIdRequest request,CancellationToken cancellationToken = default);

    Task<Response<Transaction>> Create(CreateTransactionRequest request,CancellationToken cancellationToken = default);
    Task<Response<Transaction>> Update(UpdateTransactionRequest request,CancellationToken cancellationToken = default);
    Task<Response<Transaction>> Delete(DeleteTransactionRequest request,CancellationToken cancellationToken = default);
}