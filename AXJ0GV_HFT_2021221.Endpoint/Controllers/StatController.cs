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
        //NonCRUD
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> GroupByAndCountByName()
        {
            return ownerLogic.GroupByAndCountByName();
        }
        [HttpGet]
        public List<Owner> OrderByIdentityCardNumber()
        {
            return ownerLogic.OrderByIdentityCardNumber();
        }

        //dogLogic
        //NonCRUD
        [HttpGet("{ownerID}")]
        public int CountByOwner(int ownerID)
        {
            return dogLogic.CountByOwner(ownerID);
        }
        [HttpGet("{injectionID}")]
        public int CountByInjection(int injectionID)
        {
            return dogLogic.CountByInjection(injectionID);
        }
        [HttpGet]
        public List<Injection> GetUsedInjections()
        {
            return dogLogic.GetUsedInjections();
        }



        //injectionLogic
        //NonCRUD
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
