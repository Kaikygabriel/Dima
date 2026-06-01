using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping.Identity;

internal sealed class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.ToTable("IdentityRole");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(140);

        builder.Property(x => x.ConcurrencyStamp)
            .IsConcurrencyToken();
        
        builder.Property(x=>x.NormalizedName)
            .HasColumnType("VARCHAR")
            .HasMaxLength(140);

        builder.HasIndex(x => x.NormalizedName)
            .IsUnique();
    }
}