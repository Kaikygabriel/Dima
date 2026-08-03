using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface IOrderHandler
{
    Task<Response<Order>> CreateAsync(CreateOrderRequest request);
    Task<Response<Order>> PayAsync(PayOrderRequest request);
    Task<Response<Order>> RefundAsync(RefundOrderRequest request);
    Task<Response<Order>> CancelAsync(CancelOrderRequest request,CancellationToken cancellationToken = default);
    Task<PagedResponse<List<Order>>> GetAllAsync(GetAllOrdersRequest request);
    Task<Response<Order>> GetByIdAsync(GetOrderByIdRequest request);
}