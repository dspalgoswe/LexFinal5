﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Entities
{
    public class Module
    {
public int ModuleId { get; set; }
        public string? Name { get; set; }   
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }    
        public Course? Course { get; set; }
        public ICollection<Activity>? Activities { get; set; }
        public ICollection<Document>?  Documents { get; set; }
    }

}


