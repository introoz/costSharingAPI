using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InzynierkaWebService.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace InzynierkaWebService.Controllers
{
    [Route("api/[controller]")]
    public class CostsController : Controller
    {
        //private InzynierkaContext _context;

        public ICostRepository Costs { get; set; }

        public CostsController(ICostRepository costs)//, InzynierkaContext context)
        {
            Costs = costs;
            //_context = context;
        }

        // GET: api/values
        [HttpGet(Name ="GetAll")]
        [Authorize]
        public IEnumerable<Costs> GetAll()
        {
            return Costs.GetAll();
            //return new string[] { "value1", "value2" };
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
            //return "value";
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
