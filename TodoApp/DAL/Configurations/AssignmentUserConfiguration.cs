using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class AssignmentUserConfiguration : IEntityTypeConfiguration<AssignmentUser>
    {
        public void Configure(EntityTypeBuilder<AssignmentUser> builder)
        {
            //builder.HasKey(pt => new { pt.AssignmentId, pt.AppUserId });

            //builder.HasOne(pt => pt.Assignment)
            //    .WithMany(p => p.AssignmentUsers)
            //    .HasForeignKey(pt => pt.AssignmentId);

            //builder.HasOne(pt => pt.AppUser)
            //    .WithMany(t => t.AssignmentUsers)
            //    .HasForeignKey(pt => pt.AppUserId);
        }
    }
}