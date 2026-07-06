using Dima.Api.Data.Context;
using Dima.Core.Commum.Extensions;
using Dima.Core.Enum;
using Dima.Core.Handler;
using Dima.Core.Models;
using Dima.Core.Requests.Transaction;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal sealed class TransactionHandler : ITransactionHandler
{
    private readonly AppDbContext _context;

    public TransactionHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponse<IEnumerable<Transaction>>> GetAllByCreateAt(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var start = request.Start;
        var end = request.End;
        if (start is null || end is null)
        {
            var now = DateTime.UtcNow;

            start = now.GetFirstDayOfMonth();
            end = now.GetLastDayOfMonth(); 
        }
        
        var transactions =  await _context.Transactions
            .Include(x=>x.Category)
            .Where(x => x.UserId == request.UserId && x.CreateAt >= start && x.CreateAt <= end )
            .OrderBy(x=>x.CreateAt)
            .ToListAsync(cancellationToken);
        
        var count = await _context.Transactions
            .Where(x=>x.UserId == request.UserId)
            .Where(x => x.CreateAt >= start && x.CreateAt <= end)
            .CountAsync(cancellationToken);
        
        return new PagedResponse<IEnumerable<Transaction>>(transactions, request.Page, count, request.PageSize);
    }
    
    public async Task<PagedResponse<IEnumerable<Transaction>>> GetAllByPaidOrReceivedAt(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var start = request.Start;
        var end = request.Start;
        if (start is null || end is null)
        {
            var now = DateTime.UtcNow;

            start = now.GetFirstDayOfMonth();
            end = now.GetLastDayOfMonth(); 
        }

        var transactions =  await _context.Transactions
            .Where(x=>x.UserId == request.UserId)
            .Where(x => x.PaidOrReceivedAt >= start && x.PaidOrReceivedAt <= end )
            .OrderBy(x=>x.PaidOrReceivedAt)
            .ToListAsync(cancellationToken);
        
        var count = await _context.Transactions
            .Where(x=>x.UserId == request.UserId)
            .Where(x => x.PaidOrReceivedAt >= start && x.PaidOrReceivedAt <= end)
            .CountAsync(cancellationToken);
        
        return new PagedResponse<IEnumerable<Transaction>>(transactions, request.Page, count, request.PageSize);
    }

    public async Task<Response<Transaction>> GetById(GetTransactionsByIdRequest request, CancellationToken cancellationToken = default)
    {
        var transaction =
            await _context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && request.UserId == x.UserId,cancellationToken);
        if (transaction is null)
            return new Error("Transaction.NotFound", "Not Found");
        return transaction;
    }

    public async Task<Response<Transaction>> Create(CreateTransactionRequest request,CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId && x.UserId == request.UserId,
            cancellationToken);
        
        if (category is null)
            return new Error("Category.NotFound", "Category not found");
        
        var transaction = new Transaction(request.Title,request.Type,request.Amount,category,request.UserId);
        if (transaction.EType is  ETypeTransaction.Out )
            transaction.Amount *= -1;    
        
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);
        return transaction;
    }

    public async Task<Response<Transaction>> Update(UpdateTransactionRequest request,CancellationToken cancellationToken = default)
    {
        var transactionUpdate =
            await _context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId,
                cancellationToken);
        
        if (transactionUpdate is null)
            return new Error("Transaction.NotFound", "Transaction not found");
        
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId && x.UserId == request.UserId,
            cancellationToken);
        
        if (category is null)
            return new Error("Category.NotFound", "Category not found");
        
        transactionUpdate.Title = request.Title;
        transactionUpdate.Category = category;
        transactionUpdate.EType = request.Type;
        transactionUpdate.PaidOrReceivedAt = request.PaidOrReceivedAt;
        transactionUpdate.Amount = request.Amount;
        
        if(transactionUpdate.EType is ETypeTransaction.Out)
            transactionUpdate.Amount = request.Amount * -1;
        
        _context.Transactions.Update(transactionUpdate);
        await _context.SaveChangesAsync(cancellationToken);
        return transactionUpdate;
    }

    public async Task<Response<Transaction>> Delete(DeleteTransactionRequest request,CancellationToken cancellationToken = default)
    {
        var transaction =
            await _context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId,
                cancellationToken);

        if (transaction is null)
            return new Error("Transaction.NotFound", "Transaction not found");

        _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync(cancellationToken);
        return transaction;
    }   
}