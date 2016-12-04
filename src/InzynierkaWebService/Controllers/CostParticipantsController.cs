using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InzynierkaWebService.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace InzynierkaWebService.Controllers
{
    public class CostParticipantsController : Controller
    {
        public ICostParticipantsRepository CostParticipants { get; set; }

        public CostParticipantsController(ICostParticipantsRepository costParticipants)
        {
            CostParticipants = costParticipants;
        }

        // GET: api/CostParticipants/GetCostParticipants/id
        [HttpGet("GetCostParticipants{costId}", Name = "GetAll")]
        public IEnumerable<CostParticipants> GetAll(int costId)
        {
            return CostParticipants.GetCostParticipants(costId);
        }

        //// GET api/values/5
        //[HttpGet("{id}", Name = "GetCost")]
        //public IActionResult GetById(int id)
        //{
        //    var item = Costs.Find(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return new ObjectResult(item);
        //}

        // POST api/CostParticipants/CreateCost
        [HttpPost("CreateCostParticipant", Name = "CreateCostParticipant")]
        public IActionResult Create([FromBody]CostParticipants item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            CostParticipants.Add(item);
            return CreatedAtRoute("CreateCostParticipant", /*new { id = item.CostId },*/ item);
        }

        // POST api/values
        [HttpPost("UpdateCostParticipant", Name = "UpdateCostParticipant")]
        public IActionResult Update([FromBody]CostParticipants item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            CostParticipants.Update(item);
            return CreatedAtRoute("UpdateCostParticipants", /*new { id = item.CostId },*/ item);
        }

        [HttpDelete("DeleteCost/{id}")]
        public IActionResult Delete(int id)
        {
            var item = CostParticipants.Find(id);
            //var todo = TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            CostParticipants.Remove(id);
            return new NoContentResult();
        }
    }
}
