using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface IGroupRepository
    {
        IEnumerable<GroupClone> GetByUserName(string username);
        IEnumerable<GroupClone> GetAll();
        void Add(Groups item);
        Groups Find(int key);
        bool Remove(int groupId);
        void Update(Groups item);

        Boolean SaveGroup(Groups group, string username);
    }
}
