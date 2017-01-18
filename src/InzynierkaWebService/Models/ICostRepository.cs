using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface ICostRepository
    {
        void Add(Costs item);
        IEnumerable<Costs> GetAll();
        Costs Find(int key);
        bool Remove(int key);
        void Update(Costs item);
        IEnumerable<CostClone> GetCostByInstanceId(int instanceId, string username);
        bool SaveCost(Costs item, string username, int instanceId);
    }
}
