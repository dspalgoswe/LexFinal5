using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Activity
    {
        public Activity()
        {
            Documents = new List<Document>();
        }

        public int ActivityId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }

        [Required]
        public int ModuleId { get; set; }
        public Module Module { get; set; }

        public ICollection<Document> Documents { get; set; }
    }   
}
