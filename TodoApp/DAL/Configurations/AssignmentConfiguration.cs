using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.Property(a => a.Title).IsRequired().HasMaxLength(255);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.SprintId).IsRequired();
            builder.Property(a => a.StatusId).IsRequired();
            builder.Property(a => a.StartedDate).IsRequired();
            builder.Property(a => a.ExpirationDate).IsRequired();
        }
    }
}