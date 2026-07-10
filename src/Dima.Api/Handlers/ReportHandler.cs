using Dima.Api.Data.Context;
using Dima.Core.Handler;
using Dima.Core.Models.Reports;
using Dima.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

internal sealed class ReportHandler : IReportHandler
{
    private readonly AppDbContext _appDbContext;

    public ReportHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Response<List<IncomesByCategory>>> GetIncomeByCategoryAsync(Guid userId)
    {
        var incomes = await _appDbContext.IncomesByCategory.Where(x => x.UserId == userId).ToListAsync();
        if (!incomes.Any())
            return new Error("Not Found", "NOt Found");
        return incomes;
    }

    public async Task<Response<List<ExpensesByCategory>>> GetExpensesByCategoryAsync(Guid userId)
    {
        var expenses = await _appDbContext.ExpensesByCategory.Where(x => x.UserId == userId).ToListAsync();
        if (!expenses.Any())
            return new Error("Not Found", "NOt Found");
        return expenses;
    }

    public async Task<Response<FinanceSummary>> GetFinanceSummaryAsync(Guid userId)
    {
        var financeSummary = await _appDbContext.FinanceSummary.FirstOrDefaultAsync(x=>x.UserId == userId);
        if (financeSummary is null)
            return new Error("Not Found", "Not Found");
        return financeSummary;
    }

    public async Task<Response<List<IncomeAndExpenses>>> GetIncomeAndExpensesAsync(Guid userId, int month, int year)
    {
        var expenses = await _appDbContext.IncomesAndExpenses
            .Where(x => x.UserId == userId && x.Month == month && x.Year == year)
            .ToListAsync();
        if (!expenses.Any())
            return new Error("Not Found", "NOt Found");
        return expenses;
    }
}