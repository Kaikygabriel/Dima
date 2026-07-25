using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface IVoucherHandler
{
    Task<Response<Voucher>> GetByCodeAsync(GetVoucherByCodeRequest request);
}