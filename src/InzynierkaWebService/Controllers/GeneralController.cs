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
    public class GeneralController : Controller
    {
        //private InzynierkaContext _context;

        public ICostRepository Costs { get; set; }

        public IGroupRepository Groups { get; set; }

        public GeneralController(ICostRepository costs, IGroupRepository groups)
        {
            this.Costs = costs;
            this.Groups = groups;
        }

        [HttpGet("GetAllCosts")]
        public IEnumerable<Costs> GetAllCosts()
        {
            return Costs.GetAll();
        }

        [HttpGet("GetGroupsByUsername/{username}")]
        public IEnumerable<Groups> GetGroupsByUsername(string username)
        {
            return Groups.GetByUserName(username);
        }

        [HttpGet("GetAllGroups")]
        public IEnumerable<Groups> GetAll()
        {
            //return _context.Groups.ToList();
            return Groups.GetAll();
            //return new string[] { "value1", "value2" };
        }

    }
}
