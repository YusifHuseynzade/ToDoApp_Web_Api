using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentUser> AssignmentUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Status> Statuses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.AppUserRole)
                .WithMany(r => r.AppUsers)
                .HasForeignKey(u => u.RoleId);

        }
    }
}