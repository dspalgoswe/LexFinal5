﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string? UserId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
        public Course Course { get; set; }
        public Module Module { get; set; }
        public Activity Activity { get; set; }
    }
}
