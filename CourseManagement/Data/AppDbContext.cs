using CourseManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CourseManagement.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<InstructorSocialMedia> InstructorSocialMedias { get; set; }
        public DbSet<Setting> Settings { get; set; }    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Course>()
               .Property(e => e.Price)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<InstructorSocialMedia>()
               .HasKey(ism => new { ism.InstructorId, ism.SocialMediaId });

            modelBuilder.Entity<InstructorSocialMedia>()
                .HasOne(ism => ism.SocialMedia)
                .WithMany()
                .HasForeignKey(ism => ism.SocialMediaId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Courses)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            ModelBuilder modelBuilder1 = modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            });

        }

    }

}