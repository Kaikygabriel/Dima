using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface ITransactionHandler
{
    Task<Response<Transaction>> Create(CreateTransactionRequest request,CancellationToken cancellationToken = default);
    Task<Response<Transaction>> Update(UpdateTransactionRequest request,CancellationToken cancellationToken = default);
    Task<Response<Transaction>> Delete(DeleteTransactionRequest request,CancellationToken cancellationToken = default);
}