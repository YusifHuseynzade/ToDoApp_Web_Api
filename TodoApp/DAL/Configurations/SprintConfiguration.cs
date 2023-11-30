using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
    {
        public void Configure(EntityTypeBuilder<Sprint> builder)
        {
            builder.Property(a => a.Title).IsRequired().HasMaxLength(255);

            builder.HasMany(c => c.Assignments)
               .WithOne(p => p.Sprint)
               .HasForeignKey(p => p.SprintId);
        }
    }
}