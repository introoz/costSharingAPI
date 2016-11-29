using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class CostTypes
    {
        public CostTypes()
        {
            Costs = new HashSet<Costs>();
            OperationsLog = new HashSet<OperationsLog>();
        }

        public int CostTypeId { get; set; }
        public string Name { get; set; }
        public bool EqualDivision { get; set; }
        public int? OwnerId { get; set; }
        public int? InstanceId { get; set; }

        public virtual ICollection<Costs> Costs { get; set; }
        public virtual ICollection<OperationsLog> OperationsLog { get; set; }
        public virtual Instances Instance { get; set; }
        public virtual Users Owner { get; set; }
    }
}
