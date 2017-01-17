using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class InstanceRepository : IInstanceRepository
    {
        private CostSharingContext _context;

        public InstanceRepository(CostSharingContext context)
        {
            _context = context;
        }

        public IEnumerable<InstanceClone> GetByGroupId(int groupId)
        {

            var instances = _context.Instances.Where(m => m.GroupId == groupId).ToList();
            var instanceList = new List<InstanceClone>();

            foreach (var instance in instances)
            {
                instanceList.Add(new InstanceClone
                {
                    GroupId = instance.GroupId,
                    InstanceId = instance.InstanceId,
                    Name = instance.Name         
                });
            }

            return instanceList;
        }

        public Boolean SaveInstance(Instances instance)
        {
            var foundInstance = _context.Instances.FirstOrDefault(i => i.InstanceId == instance.InstanceId);
            if (foundInstance == null)
            {
                _context.Instances.Add(new Instances
                {
                    InstanceId = _context.Instances.Last().InstanceId + 1,
                    Name = instance.Name,                    
                    GroupId = instance.GroupId
                });
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Remove(int instanceId)
        {
            var instance = _context.Instances.FirstOrDefault(i => i.InstanceId == instanceId);
            if (instance != null)
            {
                _context.Instances.Remove(instance);

                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}
