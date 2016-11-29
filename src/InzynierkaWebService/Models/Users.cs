using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class Users
    {
        public Users()
        {
            CostTypes = new HashSet<CostTypes>();
            Groups = new HashSet<Groups>();
            Members = new HashSet<Members>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CostTypes> CostTypes { get; set; }
        public virtual ICollection<Groups> Groups { get; set; }
        public virtual ICollection<Members> Members { get; set; }
    }
}
