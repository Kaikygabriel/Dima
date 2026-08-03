using Dima.Api.Data.Context;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal sealed class VoucherHandler : IVoucherHandler
{
    private readonly AppDbContext _context;

    public VoucherHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<Voucher>> GetByCodeAsync(GetVoucherByCodeRequest request)
    {
        var user = await _context.Users
            .Include(x=>x.VouchersUsed)
            .FirstOrDefaultAsync(x => x.Id == request.UserId);
        
        if (user is null)
            return new Error("User not Found","User Invalid");

        var voucher = await _context.Vouchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => request.Code == x.Code );
        
        if(voucher is null )
            return new Error("Voucher not Found","Voucher Invalid");
        
        if(!voucher.IsActive)
            return new Error("Voucher not Active","Voucher Invalid");
            
        if (user.VouchersUsed.Exists(x => x.Id == voucher.Id))
            return new Error("Voucher already use", "Voucher invalid");
        
        return voucher;
    }
}