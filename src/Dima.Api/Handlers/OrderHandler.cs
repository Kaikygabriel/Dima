using Dima.Api.Data.Context;
using Dima.Core.Enum;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal sealed class OrderHandler : IOrderHandler
{
    private readonly AppDbContext _appDbContext;

    public OrderHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Response<Order>> CreateAsync(CreateOrderRequest request)
    {
        
    }

    public Task<Response<Order>> PayAsync(PayOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Order>> RefundAsync(RefundOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Order>> CancelAsync(CancelOrderRequest request)
    {
        var order = await _appDbContext.Orders
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
        if (order is null)
            return new Error("Order Not Found","Order Not Found !");
        if(order.StatePayment is EStatePayment.Paid || 
           order.StatePayment is EStatePayment.Reversed ||
           order.StatePayment is EStatePayment.Cancel)
            return new Error("Order Not Canceled","The order cannot be cancelled.!");

        order.AlterState(EStatePayment.Cancel);
        _appDbContext.Orders.Update(order);
        await _appDbContext.SaveChangesAsync();
        
        return order;
    }

    public async Task<PagedResponse<List<Order>>> GetAllAsync(GetAllOrdersRequest request)
    {
        var query = _appDbContext.Orders
                .Where(x => x.UserId == request.UserId);

        var orders = await query.Skip((request.Page  == 0 ? 0 : request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        var count = await query.CountAsync();
        return new PagedResponse<List<Order>>()
        {
            PageSize = request.PageSize,
            CurrentPage =request.Page,
            TotalCount = count,
            Data = orders
        };
    }

    public async Task<Response<Order>> GetByIdAsync(GetOrderByIdRequest request)
    {
        var order = await _appDbContext.Orders.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
        if(order is null)
            return new Error("Order not found","Order not found");
        return order;
    }
}