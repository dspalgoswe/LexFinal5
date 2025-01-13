using System;
using System.Collections.Generic;
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
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Course? Course { get; set; }

        // Changed from Icollectoin to List<T>
        public List<Activity> Activities { get; set; }
        public List<Document> Documents { get; set; }
    }

}


