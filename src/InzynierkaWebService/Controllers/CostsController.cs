using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InzynierkaWebService.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace InzynierkaWebService.Controllers
{
    [Route("api/[controller]")]
    public class CostsController : Controller
    {
        public ICostRepository Costs { get; set; }

        public CostsController(ICostRepository costs)
        {
            Costs = costs;
        }

        // GET: api/values
        [HttpGet(Name ="GetAll")]
        public IEnumerable<Cost> GetAll()
        {
            return Costs.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCost")]
        public IActionResult GetById(int id)
        {
            var item = Costs.Find(id);
            if(item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost("CreateCost", Name ="CreateCost")]
        public IActionResult Create([FromBody]Cost item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Costs.Add(item);
            return CreatedAtRoute("CreateCost", /*new { id = item.CostId },*/ item);
        }

        // POST api/values
        [HttpPost("UpdateCost", Name = "UpdateCost")]
        public IActionResult Update([FromBody]Cost item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            Costs.Update(item);
            return CreatedAtRoute("UpdateCost", /*new { id = item.CostId },*/ item);
        }

        [HttpDelete("DeleteCost/{id}")]
        public IActionResult Delete(int id)
        {
            var item = Costs.Find(id);
            //var todo = TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            Costs.Remove(id);
            return new NoContentResult();
        }
    }
}
