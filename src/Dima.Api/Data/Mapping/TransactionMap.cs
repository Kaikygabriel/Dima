using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(x => x.Amount)
            .HasColumnName("Amount")
            .HasColumnType("MONEY")
            .IsRequired();

        builder.Property(x => x.EType)
            .HasConversion<string>()
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CreateAt)
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder.Property(x => x.PaidOrReceivedAt)
            .HasColumnType("DATETIME2")
            .IsRequired(false);
        
        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Transaction_Category")
            .IsRequired();
        
    }
}