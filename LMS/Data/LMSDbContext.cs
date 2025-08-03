using LMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(d => d.CreatedByUser)
                      .WithMany()   //User can have many courses created by them. but no collection navigation property in User
                      .HasForeignKey(d => d.CreatedBy)  //The property CreatedBy in Course is the foreign key
                      .OnDelete(DeleteBehavior.Restrict);   //Prevent deleting a User if there are courses referencing them
            });

        }
    }
}
