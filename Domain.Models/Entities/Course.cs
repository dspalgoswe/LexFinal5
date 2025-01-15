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
            Users = new List<ApplicationUser>();
            Documents = new List<Document>();
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public List<Module> Modules { get; set; }

        public List<Document> Documents { get; set; }
    }
}
