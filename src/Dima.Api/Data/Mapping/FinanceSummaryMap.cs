using Dima.Core.Models.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal sealed class FinanceSummaryMap : IEntityTypeConfiguration<FinanceSummary>
{
    public void Configure(EntityTypeBuilder<FinanceSummary> builder)
    {
        builder.ToView("VwFinanceSummary");
        builder.HasNoKey();
    }
}