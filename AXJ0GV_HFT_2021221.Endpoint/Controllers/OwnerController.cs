using AXJ0GV_HFT_2021221.Logic;
using AXJ0GV_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private IOwnerLogic logic;

        public OwnerController(IOwnerLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet("test")]
        public string Test()
        {
            return "TEST";
        }
        [HttpGet]
        public IEnumerable<Owner> GetAll()
        {
            return logic.ReadAll();
        }
        [HttpPost]
        public void AddOne([FromBody] Owner owner)
        {
            logic.Create(owner);
        }
        [HttpPut]
        public void EditOne([FromBody] Owner owner)
        {
            logic.Update(owner);
        }
        [HttpDelete("{ownerId}")]
        public void DeleteOne([FromRoute] int ownerId)
        {
            logic.Delete(ownerId);
        }
    }
}
