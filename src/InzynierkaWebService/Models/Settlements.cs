using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class Settlements
    {
        public int SettlementId { get; set; }
        public int CostId { get; set; }
        public int MemberInDebt { get; set; }
        public int MemberOwed { get; set; }
        public decimal Value { get; set; }

        public virtual Cost Cost { get; set; }
        public virtual Members MemberInDebtNavigation { get; set; }
        public virtual Members MemberOwedNavigation { get; set; }
    }
}
