using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    public class CreateActivityDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ModuleId { get; set; }

        [Required]
        public int ActivityTypeId { get; set; }
    }

    public class UpdateActivityDto
    {
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
    }

    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ModuleId { get; set; }
        public ActivityTypeDto? ActivityType { get; set; }
        public List<DocumentDto>? Documents { get; set; }
    }
}
