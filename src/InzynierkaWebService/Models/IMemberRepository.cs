using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface IMemberRepository
    {
        IEnumerable<MemberClone> GetByGroupId(int groupId);
        Boolean SaveMember(Members member);
        bool Remove(int memberId);
    }
}
