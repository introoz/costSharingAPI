using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class MemberClone
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public int? CorrespondingUserId { get; set; }
        public int GroupId { get; set; }
    }
}

