using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostClone
    {
        public int costId { get; set; }
        public string name { get; set; }
        public string creatorName { get; set; }
        public string type { get; set; }
        public decimal amount { get; set; }
        public int participantCount { get; set; }
        
    }
}
