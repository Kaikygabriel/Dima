using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping;

internal sealed class CategoryMap : IEntityTypeConfiguration<Core.Models.Category> 
{
    public void Configure(EntityTypeBuilder<Core.Models.Category> builder)
    {
        builder.ToTable("Category");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Title)
            .HasColumnType("Title")
            .HasColumnType("VARCHAR") 
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnType("Description")
            .HasColumnType("TEXT")
            .IsRequired();
    }
}