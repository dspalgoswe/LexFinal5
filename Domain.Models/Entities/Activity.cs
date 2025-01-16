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
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int ModuleId { get; set; }

        public Module Module { get; set; }

        public int ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }

        public List<Document> Documents { get; set; }
    }

    //public class ActivityTypeDto
    //{
    //    public int ActivityTypeId { get; set; }
    //    public string? Type { get; set; }
    //    public string? Deadlines { get; set; }
    //}
}
