using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostRepository : ICostRepository
    {
        private CostSharingContext _context;


        public CostRepository(CostSharingContext context)
        {
            _context = context;
            //Add(new Costs { Name = "Item1" });
        }

        public IEnumerable<Costs> GetAll()
        {
            var x = _context.Costs.ToList();
            return x;
            //return _costs.Values;
        }

        public void Add(Costs item)
        {
            item.CostId = new Random().Next();
            _context.Costs.Add(item);
            _context.SaveChanges();

            //_costs[item.CostId] = item;

            //item.Key = Guid.NewGuid().ToString();
            //_todos[item.Key] = item;
        }

        public Costs Find(int id)
        {
            Costs item;
            //_costs.TryGetValue(id, out item);
            item = _context.Costs.FirstOrDefault(x => x.CostId == id);
            return item;

            //TodoItem item;
            //_todos.TryGetValue(key, out item);
            //return item;
        }

        public bool Remove(int id)
        {
            Costs item;
            _context.Costs.Remove(this.Find(id));
            _context.SaveChanges();
            //_costs.TryRemove(id, out item);
            return true;

            //TodoItem item;
            //_todos.TryRemove(key, out item);
            //return item;
        }

        public void Update(Costs item)
        {
            //_context.Costs.FirstOrDefault(x => x == item)
            //_costs[item.CostId] = item;
            //_todos[item.Key] = item;
        }


        public IEnumerable<CostClone> GetCostByInstanceId(int instanceId, string username)
        {
            var instance = _context.Instances.FirstOrDefault(i => i.InstanceId == instanceId);
            if (instance != null)
            {
                //var group = _context.Groups.FirstOrDefault(g => g.GroupId == instance.GroupId);
                //var membersInGroup = _context.Groups.FirstOrDefault(g => g.GroupId == instance.GroupId).Members;
                var membersInGroup = _context.Members.Where(m => m.GroupId == instance.GroupId).ToList();
                var user = _context.Users.FirstOrDefault(u => u.Login.Equals(username));
                var userBelongsToGroup = membersInGroup.FirstOrDefault(m => m.CorrespondingUserId == user.UserId) != null ? true : false;
                if (userBelongsToGroup)
                {
                    var costs = _context.Costs.Where(c => c.InstanceId == instance.InstanceId).ToList();

                    var costList = new List<CostClone>();
                    foreach (var cost in costs)
                    {
                        var creator = membersInGroup.FirstOrDefault(m => m.MemberId == cost.MemberId);//_context.Members.FirstOrDefault(m => m.MemberId == cost.MemberId);
                        var participants = _context.CostParticipants.Where(c => c.CostId == cost.CostId).ToList();
                        var type = _context.CostTypes.FirstOrDefault(t => t.CostTypeId == cost.CostTypeId).Name;
                        costList.Add(new CostClone
                        {
                            costId = cost.CostId,
                            amount = cost.Amount,
                            creatorName = creator.Name,
                            name = cost.Name,
                            participantCount = participants.Count(),
                            type = type
                            //CostId = cost.CostId,
                            //Amount = cost.Amount,
                            //CostTypeId = cost.CostTypeId,
                            //InstanceId = cost.InstanceId,
                            //MemberId = cost.MemberId,
                            //Name = cost.Name
                        });
                    }
                    return costList;
                    //return instance.Costs.ToList();
                }
            }

            return new List<CostClone>();
        }

        public bool SaveCost(Costs item, string username, int instanceId)
        {
            //var cost = _context.Costs.FirstOrDefault(c => c.CostId == item.CostId);
            //if(cost == null)
            //{
            //    item.CostId = _context.Costs.Last().CostId;   
            //    _context.Users             
            //}

            //_context.Costs.Add(item);
            //_context.SaveChanges();

            return true;

            //TodoItem item;
            //_todos.TryRemove(key, out item);
            //return item;
        }
    }
}
