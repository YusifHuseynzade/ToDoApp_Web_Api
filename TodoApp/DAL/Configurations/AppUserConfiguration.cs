using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(au => au.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(au => au.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(au => au.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(au => au.RoleId)
                .IsRequired();

            builder.HasOne(au => au.AppUserRole)
                .WithMany(ar => ar.AppUsers)
                .HasForeignKey(au => au.RoleId);
        }
    }
}