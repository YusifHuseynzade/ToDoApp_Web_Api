using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configurations
{
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.Property(ar => ar.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(ar => ar.AppUsers)
                .WithOne(au => au.AppUserRole)
                .HasForeignKey(au => au.RoleId);
        }
    }
}