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
    }
}
