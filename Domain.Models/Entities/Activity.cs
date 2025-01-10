using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Activity
    {

            public int ActivityId { get; set; }

            [Required]
            public string? Name { get; set; }

            [Required]
            public string? Description { get; set; }

            [Required]
            public DateTime StartDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }

            // Navigation properties
            public Module? Module { get; set; }
            public ActivityType? ActivityType { get; set; }
            public ICollection<Document>? Documents { get; set; }
    }
}
