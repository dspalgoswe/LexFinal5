using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Domain.Models.Entities;

//Separate ApplicationUser between projects
//Setup relationship with EF here!
public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }

    // Foreign key for the course the user is enrolled in
    public int? CourseId { get; set; } 
    public Course Course { get; set; }
}