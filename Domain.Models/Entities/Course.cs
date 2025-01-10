using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Course
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        // Navigation properties

        public ICollection<ApplicationUser>? Users { get; set; }

        public ICollection<Module>? Modules { get; set; }

        public ICollection<Document>? Documents { get; set; }

    }
}
