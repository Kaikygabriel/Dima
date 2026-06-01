using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping.Identity;

internal sealed class IdentityRoleClaimMap : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("IdentityRoleClaim");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ClaimType)
            .HasMaxLength(240)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.ClaimValue)
            .HasMaxLength(240)
            .HasColumnType("VARCHAR");
    }
}