using AXJ0GV_HFT_2021221.Logic;
using AXJ0GV_HFT_2021221.Models;
using AXJ0GV_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Test
{
    class DogLogicTest
    {
        IDogLogic logic;

        Mock<IDogRepository> mockRepo =
               new Mock<IDogRepository>();
        [SetUp]
        public void Setup()
        {
            mockRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Dog>
                {
                new Dog()
                {
                    Id = 1,
                    Name = "Áfi",
                    Sex = Sex.Female,
                    Species = "Pug"
                }
        }.AsQueryable());
            logic = new DogLogic((DogRepository)mockRepo.Object);
        }
    }
}
