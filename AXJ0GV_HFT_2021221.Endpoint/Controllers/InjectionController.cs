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
    public class InjectionController : ControllerBase
    {
        private IInjectionLogic logic;
        IHubContext<SignalRHub> hub;

        public InjectionController(IInjectionLogic logic, IHubContext<SignalRHub> hub)
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
        public IEnumerable<Injection> GetAll()
        {
            return logic.ReadAll();
        }
        [HttpPost]
        public void AddOne([FromBody] Injection injection)
        {
            logic.Create(injection);
            this.hub.Clients.All.SendAsync("InjectionCreated", injection);
        }
        [HttpPut]
        public void EditOne([FromBody] Injection injection)
        {
            logic.Update(injection);
            this.hub.Clients.All.SendAsync("InjectionUpdated", injection);
        }
        [HttpDelete("{injectionId}")]
        public void DeleteOne([FromRoute] int injectionId)
        {
            var injectionToDelete = this.logic.Read(injectionId);
            logic.Delete(injectionId);
            this.hub.Clients.All.SendAsync("InjectionDeleted", injectionToDelete);
        }
    }
}
