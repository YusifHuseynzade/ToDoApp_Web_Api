using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasMaxLength(255);

            builder.HasMany(c => c.Assignments)
               .WithOne(p => p.Status)
               .HasForeignKey(p => p.StatusId);
        }
    }
}