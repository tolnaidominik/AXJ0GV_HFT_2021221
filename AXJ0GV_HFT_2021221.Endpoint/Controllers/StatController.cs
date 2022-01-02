using AXJ0GV_HFT_2021221.Logic;
using AXJ0GV_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IOwnerLogic ownerLogic;
        IDogLogic dogLogic;
        IInjectionLogic injectionLogic;

        public StatController(IOwnerLogic ownerLogic,IDogLogic dogLogic,IInjectionLogic injectionLogic)
        {
            this.ownerLogic = ownerLogic;
            this.dogLogic = dogLogic;
            this.injectionLogic = injectionLogic;
        }

        //ownerLogic
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> GroupByAndCountByName()
        {
            return ownerLogic.GroupByAndCountByName();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> CountDogs()
        {
            return ownerLogic.CountDogs();
        }
        //dogLogic
        [HttpGet("{name}")]
        public int CountByOwner(Owner owner)
        {
            return dogLogic.CountByOwner(owner);
        }
        [HttpGet("{injection}")]
        public int CountByInjection(Injection injection)
        {
            return dogLogic.CountByInjection(injection);
        }
        //injectionLogic
        [HttpGet]
        public List<Injection> OrderByPrice()
        {
            return injectionLogic.OrderByPrice();
        }
        [HttpGet]
        public int SumPrice()
        {
            return injectionLogic.SumPrice();
        }
    }
}
