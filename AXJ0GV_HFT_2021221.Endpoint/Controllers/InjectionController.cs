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
    public class InjectionController : ControllerBase
    {
        private IInjectionLogic logic;

        public InjectionController(IInjectionLogic logic)
        {
            this.logic = logic;
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
        }
        [HttpPut]
        public void EditOne([FromBody] Injection injection)
        {
            logic.Update(injection);
        }
        [HttpDelete("{injectionId}")]
        public void DeleteOne([FromRoute] int injectionId)
        {
            logic.Delete(injectionId);
        }
    }
}
