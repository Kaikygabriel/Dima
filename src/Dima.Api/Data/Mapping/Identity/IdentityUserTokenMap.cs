using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping.Identity;

internal sealed class IdentityUserTokenMap : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable("IdentityUserToken");

        builder.HasKey(x=> new {x.LoginProvider,x.UserId});
        
        builder.Property(x => x.Name)
            .HasMaxLength(160)
            .HasColumnType("VARCHAR");

        builder.Property(x => x.Value)
            .HasMaxLength(255)
            .HasColumnType("VARCHAR");
        
        builder.Property(x => x.LoginProvider)
            .HasMaxLength(180)
            .HasColumnType("VARCHAR");
    }
}