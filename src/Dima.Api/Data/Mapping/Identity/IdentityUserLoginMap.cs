using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping.Identity;

internal sealed class IdentityUserLoginMap : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("IdentityUserLogin");

        builder.HasKey(x=> new {x.LoginProvider,x.UserId,x.ProviderKey});
        
        builder.Property(x => x.LoginProvider)
            .HasMaxLength(255)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.ProviderDisplayName)
            .HasMaxLength(255)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.ProviderKey)
            .HasMaxLength(200)
            .HasColumnType("VARCHAR");
    }
}