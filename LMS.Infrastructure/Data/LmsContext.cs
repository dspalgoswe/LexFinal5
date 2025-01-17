using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data;

public class LmsContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public LmsContext(DbContextOptions<LmsContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Always call base method

        modelBuilder.Entity<ApplicationUser>()
        .HasOne(a => a.Course)  // Each ApplicationUser has one Course
        .WithMany(c => c.EnrolledUsers)  // A Course can have many ApplicationUsers
        .HasForeignKey(a => a.CourseId)  // The foreign key on ApplicationUser
        .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletes for users (if a course is deleted, do not delete users)

        // Configure the Document-Module relationship
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Course)
            .WithMany(c => c.Documents)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.SetNull);  // Optional, sets CourseId to null if the course is deleted

        modelBuilder.Entity<Document>()
            .HasOne(d => d.Module)
            .WithMany(m => m.Documents)
            .HasForeignKey(d => d.ModuleId)
            .OnDelete(DeleteBehavior.SetNull); // Optional, sets ModuleId to null if the module is deleted

        modelBuilder.Entity<Document>()
            .HasOne(d => d.Activity)
            .WithMany(a => a.Documents)
            .HasForeignKey(d => d.ActivityId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Module>()
            .Property(m => m.ModuleId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Activity>()
            .Property(a => a.ActivityId)
            .ValueGeneratedOnAdd();
    }
}