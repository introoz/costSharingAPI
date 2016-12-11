using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class CostParticipants
    {
        public CostParticipants()
        {
            OperationsLogParticipants = new HashSet<OperationsLogParticipants>();
        }

        public int DesignId { get; set; }
        public int MemberId { get; set; }
        public int CostId { get; set; }
        public decimal Paid { get; set; }
        public decimal WholeAmount { get; set; }

        public virtual ICollection<OperationsLogParticipants> OperationsLogParticipants { get; set; }
        public virtual Costs Cost { get; set; }
        public virtual Members Member { get; set; }
    }
}
