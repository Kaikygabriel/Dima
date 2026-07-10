using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal sealed class IncomesAndExpensesMap : IEntityTypeConfiguration<IncomeAndExpenses>
{
    public void Configure(EntityTypeBuilder<IncomeAndExpenses> builder)
    {
        builder.ToView("vwIncomeAndExpenses");
        builder.HasNoKey();
    }
}