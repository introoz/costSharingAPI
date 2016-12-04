using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface ICostParticipantsRepository
    {
        void Add(CostParticipants item);

        IEnumerable<CostParticipants> GetCostParticipants(int costId);
        CostParticipants Find(int key);
        bool Remove(int key);
        void Update(CostParticipants item);
    }
}
