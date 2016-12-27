using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class InstanceRepository : IInstanceRepository
    {
        private InzynierkaContext _context;

        public InstanceRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<InstanceClone> GetByGroupId(int groupId)
        {

            var instances = _context.Instances.Where(m => m.GroupId == groupId).ToList();
            var instanceList = new List<InstanceClone>();

            foreach (var instance in instances)
            {
                instanceList.Add(new InstanceClone
                {
                    GroupId = instance.GroupId,
                    InstanceId = instance.InstanceId,
                    Name = instance.Name         
                });
            }

            return instanceList;
        }

        public Boolean SaveInstance(Instances instance)
        {
            //var foundMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
            //if (foundMember == null)
            //{
            //    _context.Members.Add(new Members
            //    {
            //        MemberId = _context.Members.Last().MemberId + 1,
            //        Name = member.Name,
            //        CorrespondingUserId = member.CorrespondingUserId,
            //        GroupId = member.GroupId
            //    });
            //    _context.SaveChanges();
            //    return true;
            //}
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
