using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    // User DTOs
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [RegularExpression("^(Teacher|Student)$")]
        public string Role { get; set; }
    }

    public class UpdateUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<string> EnrolledCourses { get; set; }
    }

    // Course DTOs
    public class CreateCourseDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public List<string> UserIds { get; set; } = new();
    }

    public class UpdateCourseDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public List<string> UserIds { get; set; } = new();
    }

    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public List<UserDto> Users { get; set; }
        public List<ModuleDto> Modules { get; set; }
        public List<DocumentDto> Documents { get; set; }
    }

    // Module DTOs
    public class CreateModuleDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int CourseId { get; set; }
    }

    public class UpdateModuleDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }

    public class ModuleDto
    {
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public List<DocumentDto> Documents { get; set; }
    }

    // Activity DTOs
    public class CreateActivityDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }
    }

    public class UpdateActivityDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }
    }

    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ModuleId { get; set; }
        public ActivityTypeDto ActivityType { get; set; }
        public List<DocumentDto> Documents { get; set; }
    }

    // ActivityType DTOs
    public class ActivityTypeDto
    {
        public int ActivityTypeId { get; set; }
        public string Type { get; set; }
        public string Deadlines { get; set; }
    }

    // Document DTOs
    public class CreateDocumentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
    }

    public class UpdateDocumentDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
    }

    public class DocumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ApplicationUserId { get; set; }
        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
    }
}
