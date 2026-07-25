using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal class VoucherMap : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Voucher");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
        
        builder.Property(x => x.Amount)
            .HasColumnType("MONEY")
            .IsRequired();
        
        builder.Property(x => x.Code)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType("VARCHAR")
            .HasMaxLength(355)
            .IsRequired();
        
        builder.Property(x => x.StartDate)
            .HasColumnType("DATETIME2")
            .IsRequired();
        
        builder.Property(x => x.EndDate)
            .HasColumnType("DATETIME2")
            .IsRequired();
    }
}