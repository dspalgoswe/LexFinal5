using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Module
    {
        public Module()
        {
            Activities = new List<Activity>();
            Documents = new List<Document>();
        }

        public int ModuleId { get; set; }

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

        public Course Course { get; set; }

        public List<Activity> Activities { get; set; }

        public List<Document> Documents { get; set; }
    }

}


