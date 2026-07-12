namespace Dima.Core.Models.Reports;

public record FinanceSummary(Guid UserId,decimal Income,decimal Expense )
{
    public decimal Total => Income - (Expense < 0 ? -Expense : Expense);
}