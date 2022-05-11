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
    public class DogController : ControllerBase
    {
        private IDogLogic logic;
        IHubContext<SignalRHub> hub;

        public DogController(IDogLogic logic, IHubContext<SignalRHub> hub)
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
        public IEnumerable<Dog> GetAll()
        {
            return logic.ReadAll();
        }
        [HttpPost]
        public void AddOne([FromBody] Dog dog)
        {
            logic.Create(dog);
            this.hub.Clients.All.SendAsync("DogCreated", dog);
        }
        [HttpPut]
        public void EditOne([FromBody] Dog dog)
        {
            logic.Update(dog);
            this.hub.Clients.All.SendAsync("DogUpdated", dog);
        }
        [HttpDelete("{dogId}")]
        public void DeleteOne([FromRoute] int dogId)
        {
            var dogToDelete = this.logic.Read(dogId);
            logic.Delete(dogId);
            this.hub.Clients.All.SendAsync("DogDeleted", dogToDelete);
        }
    }
}
