using System;
using System.Collections.Generic;

namespace InzynierkaWebService.Models
{
    public partial class Notes
    {
        public int NoteId { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public int OwnerId { get; set; }
        public int? CostParentId { get; set; }
        public int? NoteParentId { get; set; }
        public string ImagePath { get; set; }

        public virtual Cost CostParent { get; set; }
        public virtual Notes Note { get; set; }
        public virtual Notes InverseNote { get; set; }
        public virtual Members Owner { get; set; }
    }
}
