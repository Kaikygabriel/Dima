using Microsoft.EntityFrameworkCore;
using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal sealed class ExpensesByCategoryMap : IEntityTypeConfiguration<ExpensesByCategory>
{
    public void Configure(EntityTypeBuilder<ExpensesByCategory> builder)
    {
        builder.ToView("VwExpensesByCategory");

        builder.HasNoKey();
    }
}