using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    // Course DTOs
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; init; }
        public List<UserDto>? Users { get; set; }
        public List<ModuleDto>? Modules { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }
    public class CreateCourseDto
    {
        [Required]
        [MinLength(1)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; init; }
        public List<UserDto>? Users { get; set; }
        public List<ModuleDto>? Modules { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }

    public class UpdateCourseDto
    {

        [Required]
        [MinLength(1)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; init; }
        public List<UserDto>? Users { get; set; }
        public List<ModuleDto>? Modules { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }

}
