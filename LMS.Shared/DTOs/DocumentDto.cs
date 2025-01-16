using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs
{
    public class CreateDocumentDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Type { get; set; }

        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
    }

    public class UpdateDocumentDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Type { get; set; }
    }

    public class DocumentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? ApplicationUserId { get; set; }
        public int? ModuleId { get; set; }
        public int? CourseId { get; set; }
        public int? ActivityId { get; set; }
    }
}
