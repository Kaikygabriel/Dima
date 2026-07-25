using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType("VARCHAR")
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(x => x.Price)
            .HasColumnType("MONEY")
            .IsRequired();
        
        builder.Property(x => x.IsActive)
            .HasColumnType("BIT")
            .IsRequired();
    }
}