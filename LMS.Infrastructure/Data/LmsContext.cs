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
        base.OnModelCreating(modelBuilder);

        // Configure many-to-many relationship between Users and Courses
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.Courses)
            .WithMany(c => c.Users);

        // Configure one-to-many relationship between Course and Modules
        modelBuilder.Entity<Module>()
            .HasOne(m => m.Course)
            .WithMany(c => c.Modules)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure one-to-many relationship between Module and Activities
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.Module)
            .WithMany(m => m.Activities)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure one-to-many relationship between ActivityType and Activities
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.ActivityType)
            .WithMany(at => at.Activities)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Document relationships
        modelBuilder.Entity<Document>()
            .HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Document>()
            .HasOne(d => d.Course)
            .WithMany(c => c.Documents)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Document>()
            .HasOne(d => d.Module)
            .WithMany(m => m.Documents)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Document>()
            .HasOne(d => d.Activity)
            .WithMany(a => a.Documents)
            .OnDelete(DeleteBehavior.SetNull);
    }
}