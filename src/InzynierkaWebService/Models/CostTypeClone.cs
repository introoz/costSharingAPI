using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostTypeClone
    {
        public int CostTypeId { get; set; }
        public string Name { get; set; }
        public bool EqualDivision { get; set; }
        public int? OwnerId { get; set; }
        public int? InstanceId { get; set; }
    }
}
