using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InzynierkaWebService.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Instances = new HashSet<Instances>();
            Members = new HashSet<Members>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int GroupOwner { get; set; }

        public virtual ICollection<Instances> Instances { get; set; }
        public virtual ICollection<Members> Members { get; set; }                
        public virtual Users GroupOwnerNavigation { get; set; }
    }
}
