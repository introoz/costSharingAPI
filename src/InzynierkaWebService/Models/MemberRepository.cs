using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class MemberRepository : IMemberRepository
    {
        private InzynierkaContext _context;

        public MemberRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<MemberClone> GetByGroupId(int groupId)
        {

            var members = _context.Members.Where(m => m.GroupId == groupId).ToList();
            // int? userId = _context.Users.FirstOrDefault(u => u.Login == username)?.UserId;
            // var members = _context.Members.Where(m => m.CorrespondingUserId == userId).Select(m => m.GroupId).ToList();

            //_context.Groups.Include(g => g.GroupOwnerNavigation);            
            // var groups = _context.Groups.Where(g => members.Contains(g.GroupId)).ToList();

            var memberList = new List<MemberClone>();
            foreach (var member in members)
            {
                memberList.Add(new MemberClone
                {
                     MemberId = member.MemberId,
                     Name = member.Name,
                     CorrespondingUserId = member.CorrespondingUserId,
                     GroupId = member.GroupId

                    // GroupId = group.GroupId,
                    // GroupName = group.GroupName,
                    // GroupOwner = group.GroupOwner
                });
            }

            //var groups = from g in _context.Groups
            //             where members.Contains(g.GroupId)
            //             select g;

            //groups.

            //var groupsList = groups.ToList();

            return memberList;
        }
    }
}
