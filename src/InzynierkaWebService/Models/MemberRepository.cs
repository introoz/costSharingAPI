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

        public Boolean SaveMember(Members member)
        {
            var foundMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);

            var mem = new Members
            {
                MemberId = _context.Members.Last().MemberId + 1,
                Name = member.Name,
                CorrespondingUserId = member.CorrespondingUserId,
                GroupId = member.GroupId
            };

            if (foundMember == null)
            {
                _context.Members.Add(mem);
                //_context.Members.Add(new Members
                //{
                //    MemberId = _context.Members.Last().MemberId + 1,
                //    Name = member.Name,
                //    CorrespondingUserId = member.CorrespondingUserId,
                //    GroupId = member.GroupId
                //});
                _context.SaveChanges();
                return true;
            }
            return false;                        
        }
        public bool Remove(int memberId)
        {
            var member = _context.Members.FirstOrDefault(m => m.MemberId == memberId);
            if (member != null)
            {
                _context.Members.Remove(member);

                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
