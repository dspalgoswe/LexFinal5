using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class ActivityType
    {
        public int ActivityTypeId { get; set; }
        [Required]
        public string? Type { get; set; }

        public string? Deadlines { get; set; }

        // Navigation properties
        public ICollection<Activity>? Activities { get; set; }
    }
}