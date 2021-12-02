﻿using AXJ0GV_HFT_2021221.Logic;
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
    public class DogController : ControllerBase
    {
        private IDogLogic logic;

        public DogController(IDogLogic logic)
        {
            this.logic = logic;
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
        }
        [HttpPut]
        public void EditOne([FromBody] Dog dog)
        {
            logic.Update(dog);
        }
        [HttpDelete("{dogId}")]
        public void DeleteOne([FromRoute] int dogId)
        {
            logic.Delete(dogId);
        }
    }
}
