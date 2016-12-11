using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class GroupRepository: IGroupRepository
    {
        private InzynierkaContext _context;

        public GroupRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<Groups> GetByUserName(string username)
        {
            int? userId = _context.Users.FirstOrDefault(u => u.Login == username)?.UserId;
            var members = _context.Members.Where(m => m.CorrespondingUserId == userId).Select(m => m.GroupId);

            var groups = from g in _context.Groups
                         where members.Contains(g.GroupId)
                         select g;

            return groups.ToList();
        }

        public IEnumerable<Groups> GetAll()
        {
            var x = _context.Groups.ToList();
            return x;
        }

        public void Add(Groups item)
        {
            ;
        }
        public Groups Find(int key)
        {
            return new Groups();
        }
        public bool Remove(int key)
        {
            return true;
        }
        public void Update(Groups item)
        {
            ;
        }
    }
}
