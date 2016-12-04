using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class Cost
    {
        public Cost()
        {
            CostParticipants = new HashSet<CostParticipants>();
            Notes = new HashSet<Notes>();
            OperationsLog = new HashSet<OperationsLog>();
            Settlements = new HashSet<Settlements>();
        }

        public int CostId { get; set; }
        public string Name { get; set; }
        public int InstanceId { get; set; }
        public int MemberId { get; set; }
        public int? CostTypeId { get; set; }
        public decimal Amount { get; set; }

        public virtual ICollection<CostParticipants> CostParticipants { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
        public virtual ICollection<OperationsLog> OperationsLog { get; set; }
        public virtual ICollection<Settlements> Settlements { get; set; }
        public virtual CostTypes CostType { get; set; }
        public virtual Instances Instance { get; set; }
        public virtual Members Member { get; set; }
    }
}
