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
        public Course()
        {
            Modules = new List<Module>();
            Documents = new List<Document>();
            EnrolledUsers = new List<ApplicationUser>();
        }

        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Navigation property for enrolled users
        public ICollection<ApplicationUser> EnrolledUsers { get; set; }

        public ICollection<Module> Modules { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}