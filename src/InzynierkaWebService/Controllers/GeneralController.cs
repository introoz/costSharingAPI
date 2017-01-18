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
        public IMemberRepository Members { get; set; }
        public IInstanceRepository Instances { get; set; }
        public IUserRepository Users { get; set; }
        public ICostTypeRepository CostTypes { get; set; }


        public GeneralController(ICostRepository costs, IGroupRepository groups, IMemberRepository members, IUserRepository users, IInstanceRepository instances, ICostTypeRepository costTypes)
        {
            this.Costs = costs;
            this.Groups = groups;
            this.Members = members;
            this.Users = users;
            this.Instances = instances;
            this.CostTypes = costTypes;
        }

        [HttpGet("GetAllCosts")]
        public IEnumerable<Costs> GetAllCosts()
        {
            return Costs.GetAll();
        }

        [HttpGet("GetGroupsByUsername/{username}")]
        public IEnumerable<GroupClone> GetGroupsByUsername(string username)
        {
            var x = Groups.GetByUserName(username);
            return x;
        }

        [HttpGet("GetAllGroups")]
        public IEnumerable<GroupClone> GetAll()
        {
            //return _context.Groups.ToList();
            return Groups.GetAll();
            //return new string[] { "value1", "value2" };
        }

        [HttpGet("GetMembersByGroupId/{groupId}")]
        public IEnumerable<MemberClone> GetMembersByGroupId(int groupId)
        {
            //return _context.Groups.ToList();
            return Members.GetByGroupId(groupId);
            //return new string[] { "value1", "value2" };
        }

        [HttpPost("SaveGroup/{username}")]
        public IActionResult SaveGroup([FromBody] Groups group, string username)
        {
            if (group == null)
            {
                return BadRequest();
            }

            Groups.SaveGroup(group, username);

            return new OkResult();
        }

        [HttpGet("DeleteGroup/{groupId}")]
        public IActionResult DeleteGroup(int groupId)
        {
            bool flag = Groups.Remove(groupId);
            if (flag)
                return new OkResult();
            else
                return new NotFoundResult();
        }

        [HttpPost("SaveMember")]
        public IActionResult SaveMember([FromBody] Members member)
        {
            if (member == null)
            {
                return BadRequest();
            }

            //Groups.SaveGroup(group, username);
            Members.SaveMember(member);

            return new OkResult();
        }

        [HttpGet("DeleteMember/{memberId}")]
        public IActionResult DeleteMember(int memberId)
        {
            bool flag = Members.Remove(memberId);
            if (flag)
                return new OkResult();
            else
                return new NotFoundResult();
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<Users> GetAllUsers()
        {
            return Users.GetAll();
        }

        [HttpGet("GetUsersByGroup/{groupId}")]
        public IEnumerable<UserClone> GetByGroup(int groupId)
        {
            return Users.GetByGroup(groupId);
        }

        [HttpGet("GetInstancesByGroupId/{groupId}")]
        public IEnumerable<InstanceClone> GetInstancesByGroupId(int groupId)
        {
            return Instances.GetByGroupId(groupId);
        }

        [HttpPost("SaveInstance")]
        public IActionResult SaveInstance([FromBody] Instances instance)
        {
            if (instance == null)
            {
                return BadRequest();
            }

            //Groups.SaveGroup(group, username);
            Instances.SaveInstance(instance);

            return new OkResult();
        }

        [HttpGet("DeleteInstance/{instanceId}")]
        public IActionResult DeleteInstance(int instanceId)
        {
            bool flag = Instances.Remove(instanceId);
            if (flag)
                return new OkResult();
            else
                return new NotFoundResult();
        }
                        
        [HttpGet("GetCostByInstanceId/{instanceId}/{username}")]
        public IEnumerable<CostClone> GetCostByInstanceId(int instanceId, string username)
        {
            return Costs.GetCostByInstanceId(instanceId, username);
            //return Costs.GetAll();
            //return new string[] { "value1", "value2" };
        }

        [HttpGet("GetCostTypes/{instanceId}/{username}")]
        public IEnumerable<CostTypeClone> getCostTypes(int instanceId, string username)        
        {
            return CostTypes.getCostTypes(instanceId, username);
            //return Costs.GetCostByInstanceId(instanceId, username);            
        }

        [HttpPost("SaveCost/{instanceId}/{username}")]
        public IActionResult SaveCost([FromBody] Costs cost, int instanceId, string username)
        {
            if (cost == null)
            {
                return BadRequest();
            }

            Costs.SaveCost(cost, username, instanceId);
            //Groups.SaveGroup(group, username);

            return new OkResult();
        }
    }


}
