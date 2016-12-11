using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface IGroupRepository
    {
        IEnumerable<Groups> GetByUserName(string username);
        IEnumerable<Groups> GetAll();
        void Add(Groups item);
        Groups Find(int key);
        bool Remove(int key);
        void Update(Groups item);
    }
}
