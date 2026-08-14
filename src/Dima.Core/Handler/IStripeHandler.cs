using Dima.Core.Requests.Stripe;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface IStripeHandler
{
    Task<Response<string>> CreateSession(CreateSessionRequest request);
    Task<Response<List<StripeTransactionReponse>>> GetTransactionsByOrder(GetTransactionsByOrderRequest request);
}