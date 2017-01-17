using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface ICostTypeRepository
    {
        IEnumerable<CostTypeClone> getCostTypes(int instanceId, string username);        
    }
}
