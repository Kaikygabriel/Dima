using Dima.Api.Models;
using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mapping.Identity;

public class IdentityUserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUser");

        builder.HasKey(x => x.Id) ;
        
        builder.Property(x => x.UserName)
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(x => x.NormalizedUserName)
            .HasColumnType("VARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(x => x.PasswordHash)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
        
        builder.Property(x => x.NormalizedEmail)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
        
        builder.Property(x => x.EmailConfirmed)
            .HasColumnType("BIT");

        builder.Property(x => x.PhoneNumber)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.ConcurrencyStamp)
            .IsConcurrencyToken();

        builder.HasMany(x => x.VouchersUsed)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "VoucherUser", 
                x=>
                                x.HasOne<Voucher>()
                                .WithMany()
                                .HasForeignKey("VoucherId")
                                .HasConstraintName("FK_VoucherUser_VoucherId")
                                .OnDelete(DeleteBehavior.Cascade)
                , 
                x=>
                                  x.HasOne<User>()
                                  .WithMany()
                                  .HasForeignKey("UserId")
                                  .HasConstraintName("FK_VoucherUser_UserId")
                                  .OnDelete(DeleteBehavior.Cascade),
                
        x =>
                                 x.HasKey("VoucherId", "UserId")
        );
        
        builder.HasIndex(x => x.NormalizedUserName)
            .IsUnique();
        
        builder.HasIndex(x => x.NormalizedEmail)
            .IsUnique();

        builder.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
    }
}