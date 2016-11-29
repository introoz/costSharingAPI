using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class OperationsLogParticipants
    {
        public int OperationsLogParticipantId { get; set; }
        public int OperationLogId { get; set; }
        public int ParticipantOperationType { get; set; }
        public int ParticipantId { get; set; }
        public decimal? Paid { get; set; }
        public decimal? WholeAmount { get; set; }

        public virtual OperationsLog OperationLog { get; set; }
        public virtual CostParticipants Participant { get; set; }
    }
}
