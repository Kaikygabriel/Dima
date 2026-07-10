using Dima.Core.Models.Reports;
using Dima.Core.Response;

namespace Dima.Core.Handler;

public interface IReportHandler
{
    Task<Response<List<IncomesByCategory>>> GetIncomeByCategoryAsync(Guid userId);
    Task<Response<List<ExpensesByCategory>>> GetExpensesByCategoryAsync(Guid userId);
    Task<Response<FinanceSummary>> GetFinanceSummaryAsync(Guid userId);
    Task<Response<List<IncomeAndExpenses>>> GetIncomeAndExpensesAsync(Guid userId,int month,int year);
}