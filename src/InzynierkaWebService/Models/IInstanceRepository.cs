using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public interface IInstanceRepository
    {
        IEnumerable<InstanceClone> GetByGroupId(int groupId);
        Boolean SaveInstance(Instances instance);
        bool Remove(int instanceId);
    }
}
