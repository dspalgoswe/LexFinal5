using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public List<string>? EnrolledCourses { get; set; }
    }
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        [RegularExpression("^(Teacher|Student)$")]
        public string? Role { get; set; }
    }

    public class UpdateUserDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
