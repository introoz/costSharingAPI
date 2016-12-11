using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class OperationsLog
    {
        public OperationsLog()
        {
            OperationsLogParticipants = new HashSet<OperationsLogParticipants>();
        }

        public int OperationsLogId { get; set; }
        public int CostId { get; set; }
        public int MemberId { get; set; }
        public int OperationType { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public int? CostTypeId { get; set; }
        public bool ParticipantsChanged { get; set; }

        public virtual ICollection<OperationsLogParticipants> OperationsLogParticipants { get; set; }
        public virtual Costs Cost { get; set; }
        public virtual CostTypes CostType { get; set; }
        public virtual Members Member { get; set; }
    }
}
