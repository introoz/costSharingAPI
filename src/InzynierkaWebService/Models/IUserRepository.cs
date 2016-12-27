using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAll();

        IEnumerable<UserClone> GetByGroup(int groupId);
    }
}
