using AXJ0GV_HFT_2021221.Logic;
using AXJ0GV_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;

        public OwnerController(IOwnerLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("OwnerCreated", owner);
        }
        [HttpPut]
        public void EditOne([FromBody] Owner owner)
        {
            logic.Update(owner);
            this.hub.Clients.All.SendAsync("OwnerUpdated", owner);
        }
        [HttpDelete("{ownerId}")]
        public void DeleteOne([FromRoute] int ownerId)
        {
            var ownerToDelete = this.logic.Read(ownerId);
            logic.Delete(ownerId);
            this.hub.Clients.All.SendAsync("OwnerDeleted", ownerToDelete);
        }
    }
}
