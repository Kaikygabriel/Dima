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
        var user = await _appDbContext.Users
            .Include(x => x.VouchersUsed)
            .FirstOrDefaultAsync(x => x.Id == request.UserId);

        if (user is null)
            return new Error("User Not Found", "User Not Found!");

        Voucher? voucher = null;
        if (!user.VouchersUsed.Exists(x => x.Id == request.VoucherId))
            voucher = await _appDbContext.Vouchers
                .Where(x=>x.StartDate >= DateTime.Now && x.EndDate <= DateTime.Now)
                .FirstOrDefaultAsync(x => x.Id == request.VoucherId);
        
        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

        if (product is null)
            return new Error("Product Not Found", "Product Not Found!");

        _appDbContext.Attach(product);
        
        var order = new Order(product, user.Id, voucher);
        _appDbContext.Orders.Add(order);
        await _appDbContext.SaveChangesAsync();
        
        return order;
    }

    public async Task<Response<Order>> PayAsync(PayOrderRequest request)
    {
        var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
        if (order is null)
            return new Error("Order not Found", "Not Found !");
        order.AlterState(EStatePayment.Paid);
        
        _appDbContext.Update(order);
        await _appDbContext.SaveChangesAsync();

        return order;
    }

    public async Task<Response<Order>> RefundAsync(RefundOrderRequest request)
    {
        var order = await _appDbContext.Orders.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
        if (order is null)
            return new Error("Order not Found", "Not Found !");
        
        if(order.StatePayment is not EStatePayment.Paid)
            return new Error("Order not paid", "Not paid for be refound !");
        
        order.AlterState(EStatePayment.Reversed);
        
        _appDbContext.Update(order);
        await _appDbContext.SaveChangesAsync();
  
        return order;
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