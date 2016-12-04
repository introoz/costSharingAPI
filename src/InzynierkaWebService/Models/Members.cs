using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class Members
    {
        public Members()
        {
            CostParticipants = new HashSet<CostParticipants>();
            Costs = new HashSet<Cost>();
            Notes = new HashSet<Notes>();
            OperationsLog = new HashSet<OperationsLog>();
            SettlementsMemberInDebtNavigation = new HashSet<Settlements>();
            SettlementsMemberOwedNavigation = new HashSet<Settlements>();
        }

        public int MemberId { get; set; }
        public string Name { get; set; }
        public int? CorrespondingUserId { get; set; }
        public int GroupId { get; set; }

        public virtual ICollection<CostParticipants> CostParticipants { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
        public virtual ICollection<OperationsLog> OperationsLog { get; set; }
        public virtual ICollection<Settlements> SettlementsMemberInDebtNavigation { get; set; }
        public virtual ICollection<Settlements> SettlementsMemberOwedNavigation { get; set; }
        public virtual Users CorrespondingUser { get; set; }
        public virtual Groups Group { get; set; }
    }
}
