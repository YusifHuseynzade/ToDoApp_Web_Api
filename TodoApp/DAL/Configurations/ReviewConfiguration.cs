using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.CreatedAt)
            .IsRequired();

            builder.Property(r => r.Text)
                .HasMaxLength(1000);

            builder.HasOne(r => r.Assignment)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.AssignmentId);

            builder.HasOne(r => r.AppUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.AppUserId);
        }
    }
}