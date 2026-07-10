namespace Dima.Core.Models.Reports;

public record IncomeAndExpenses(Guid UserId,int Month,int Year,decimal Incomes, decimal Expenses);