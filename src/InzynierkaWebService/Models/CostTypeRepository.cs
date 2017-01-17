using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostTypeRepository : ICostTypeRepository
    {
        private CostSharingContext _context;


        public CostTypeRepository(CostSharingContext context)
        {
            _context = context;
        }

        public IEnumerable<CostTypeClone> getCostTypes(int instanceId, string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login.Equals(username));
            if (user != null)
            {
                var costTypes = from c in _context.CostTypes
                                where c.OwnerId == null || c.OwnerId == user.UserId || c.InstanceId == instanceId
                                select c;

                //return costTypes.ToList();
                var costTypesList = new List<CostTypeClone>();
                foreach (var costType in costTypes)
                {
                    costTypesList.Add(new CostTypeClone
                    {
                        CostTypeId = costType.CostTypeId,
                        EqualDivision = costType.EqualDivision,
                        InstanceId = costType.InstanceId,
                        Name = costType.Name,
                        OwnerId = costType.OwnerId
                    });
                }

                return costTypesList;
            }
            return new List<CostTypeClone>();
        }

        //public IEnumerable<Costs> GetAll()
        //{
        //    var x = _context.Costs.ToList();
        //    return x;
        //    //return _costs.Values;
        //}        
    }
}
