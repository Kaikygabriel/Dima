using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        
        builder.ToTable("Order");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreateAt)
            .HasColumnType("DATETIME2")
            .IsRequired();
        
        builder.Property(x => x.UpdateAt)
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder.Property(x => x.ExternalReference)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired(false);
        
        builder.Property(x => x.StatePayment)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.PaymentGateway)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(x => x.Voucher)
            .WithMany()
            .HasForeignKey(x=>x.VoucherId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
        
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}