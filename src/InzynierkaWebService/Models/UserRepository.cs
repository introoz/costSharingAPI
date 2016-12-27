using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class UserRepository : IUserRepository
    {
        private InzynierkaContext _context;

        public UserRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAll()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public IEnumerable<UserClone> GetByGroup(int groupId)
        {
            var members = _context.Members.Where(m => m.GroupId == groupId);
            var memberIds = members.Where(m => m.CorrespondingUserId != null).Select(m => m.CorrespondingUserId);//.ToList();

            var users = _context.Users.Where(u => !memberIds.Contains(u.UserId)).ToList();

            var usersList = new List<UserClone>();
            foreach (var user in users)
            {
                usersList.Add(new UserClone
                {
                    UserId = user.UserId,
                    Login = user.Login,
                    Name = user.Name,
                    Surname = user.Surname                    
                });
            }

            return usersList;
        }
    }
}
