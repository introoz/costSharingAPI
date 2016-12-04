using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostsRepository : ICostRepository
    {
        private InzynierkaContext _context;


        public CostsRepository(InzynierkaContext context)
        {
            _context = context;
        }

        public IEnumerable<Cost> GetAll()
        {
            var x = _context.Costs.ToList();
            return x;
        }

        public void Add(Cost item)
        {
            _context.Costs.Add(item);
            _context.SaveChanges();
        }

        public Cost Find(int id)
        {
            Cost item;
            item =_context.Costs.FirstOrDefault(x => x.CostId == id);
            return item;
        }

        public bool Remove(int id)
        {
            _context.Costs.Remove(this.Find(id));
            _context.SaveChanges();
            return true;
        }

        //TODO: Zmienic na update z remove/add
        public void Update(Cost updatedItem)
        {
            Cost item =_context.Costs.FirstOrDefault(x => x.CostId == updatedItem.CostId);

            if (item != null && !item.Equals(updatedItem))
            {
                _context.Costs.Remove(item);
                _context.SaveChanges();

                _context.Costs.Add(updatedItem);
                _context.SaveChanges();
            }            
        }
    }
}
