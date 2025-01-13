using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Domain.Models.Entities;

//Separate ApplicationUser between projects
//Setup relationship with EF here!
public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpireTime { get; set; }

    public  ICollection<Course>? Courses { get; set; }
    public ICollection<Document>? Documents { get; set; }
 
}