using Dima.Api.Data.Context;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal sealed class ProductHandler : IProductHandler
{
    private readonly AppDbContext _context;

    public ProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<List<Product>>> GetAllAsync(GetAllProductsRequest request)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Skip((request.Page == 0 ? request.Page : request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
        
        var count = await _context.Products.CountAsync();
        
        return new PagedResponse<List<Product>>()
        {
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalCount = count,
            Data= products
        };
    }

    public async Task<Response<Product>> GetByIdAsync(GetProductByIdRequest request)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
        if(product is null)
            return new Error("Product Not Found !","Product Not Found !");

        return product;
    }
}