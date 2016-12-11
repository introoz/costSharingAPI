using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class Instances
    {
        public Instances()
        {
            Costs = new HashSet<Costs>();
            CostTypes = new HashSet<CostTypes>();
        }

        public int InstanceId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Costs> Costs { get; set; }
        public virtual ICollection<CostTypes> CostTypes { get; set; }
        public virtual Groups Group { get; set; }
    }
}
