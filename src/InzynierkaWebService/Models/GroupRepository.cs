using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class GroupRepository : IGroupRepository
    {
        private InzynierkaContext _context;

        public GroupRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<GroupClone> GetByUserName(string username)
        {
            int? userId = _context.Users.FirstOrDefault(u => u.Login == username)?.UserId;
            var members = _context.Members.Where(m => m.CorrespondingUserId == userId).Select(m => m.GroupId).ToList();

            //_context.Groups.Include(g => g.GroupOwnerNavigation);            
            var groups = _context.Groups.Where(g => members.Contains(g.GroupId)).ToList();

            var groupList = new List<GroupClone>();
            foreach (var group in groups)
            {
                groupList.Add(new GroupClone
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    GroupOwner = group.GroupOwner
                });
            }

            //var groups = from g in _context.Groups
            //             where members.Contains(g.GroupId)
            //             select g;

            //groups.

            //var groupsList = groups.ToList();

            return groupList;
        }

        public IEnumerable<GroupClone> GetAll()
        {
            //_context.Groups.
            var groups = _context.Groups.ToList();// .Include(g => g.GroupOwnerNavigation);
            var groupList = new List<GroupClone>();
            foreach (var group in groups)
            {
                groupList.Add(new GroupClone
                {
                    GroupId = group.GroupId,
                    GroupName = group.GroupName,
                    GroupOwner = group.GroupOwner
                });
            }
            return groupList;
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
