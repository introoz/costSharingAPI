﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class GroupRepository : IGroupRepository
    {
        private CostSharingContext _context;

        public GroupRepository(CostSharingContext context)
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

        public Boolean SaveGroup(Groups item, string username)
        {
            var group = _context.Groups.FirstOrDefault(x => x.GroupId == item.GroupId);
            if (group != null)
            {
                group.GroupName = item.GroupName;
                _context.SaveChanges();
            }
            else
            {
                
                if(_context.Groups.Count() != 0)                                                    
                    item.GroupId = _context.Groups.Last().GroupId + 1;
                else
                    item.GroupId = 1;
                var user = _context.Users.FirstOrDefault(u => u.Login == username);
                item.GroupOwner = user.UserId;
                _context.Groups.Add(item);

                int memberId = 1;
                if (_context.Members.Count() != 0)
                    memberId = _context.Groups.Last().GroupId + 1;
                _context.Members.Add(new Members
                {
                    MemberId = memberId,
                    CorrespondingUserId = user.UserId,
                    GroupId = item.GroupId,
                    Name = user.Login
                });
                _context.SaveChanges();
            }
            return true;
        }


        public void Add(Groups item)
        {
            ;
        }
        public Groups Find(int key)
        {
            return new Groups();
        }
        public bool Remove(int groupId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group != null)
            {
                var groupMembers =_context.Members.Where(m => m.GroupId == groupId);

                _context.Members.RemoveRange(groupMembers);
                _context.Groups.Remove(group);
                
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public void Update(Groups item)
        {
            ;
        }
    }
}
