using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostParticipantsRepository : ICostParticipantsRepository
    {
        private InzynierkaContext _context;


        public CostParticipantsRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<CostParticipants> GetCostParticipants(int costId)
        {
            var costPaticipantsList = _context.CostParticipants.Where(x => x.CostId == costId).ToList();            
            return costPaticipantsList;
        }

        public void Add(CostParticipants item)
        {
            _context.CostParticipants.Add(item);
            _context.SaveChanges();
        }

        public CostParticipants Find(int id)
        {
            CostParticipants item;
            item = _context.CostParticipants.FirstOrDefault(x => x.CostId == id);
            return item;
        }

        public bool Remove(int id)
        {
            _context.CostParticipants.Remove(this.Find(id));
            _context.SaveChanges();
            return true;
        }

        //TODO: Zmienic na update z remove/add
        public void Update(CostParticipants updatedItem)
        {
            CostParticipants item =_context.CostParticipants.FirstOrDefault(x => x.DesignId == updatedItem.DesignId);

            if (item != null && !item.Equals(updatedItem))
            {
                _context.CostParticipants.Remove(item);
                _context.SaveChanges();

                _context.CostParticipants.Add(updatedItem);
                _context.SaveChanges();
            }            
        }
    }
}
