using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InzynierkaWebService.Models
{
    public class CostRepository : ICostRepository
    {
        private InzynierkaContext _context;


        public CostRepository(InzynierkaContext context)
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
            item =_context.Costs.FirstOrDefault(x => x.CostId == id);
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
    }
}
