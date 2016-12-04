using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface ICostRepository
    {
        void Add(Cost item);
        IEnumerable<Cost> GetAll();
        Cost Find(int key);
        bool Remove(int key);
        void Update(Cost item);
    }
}
