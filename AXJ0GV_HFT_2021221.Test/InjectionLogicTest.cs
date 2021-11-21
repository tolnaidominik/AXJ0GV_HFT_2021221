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
    [TestFixture]
    class InjectionLogicTest
    {
        IInjectionLogic logic;

        [SetUp]
        public void Setup() 
        {
            Mock<IInjectionRepository> mockRepo =
                new Mock<IInjectionRepository>();

            mockRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Injection>
                {
            new Injection()
            {
                Id = 1,
                Name = InjectionName.Bordetella_Bronchiseptica,
                Commonness = Commonness.Once,
                Price = 5000
            },
            new Injection()
            {
                Id = 2,
                Name = InjectionName.Canine_Distemper,
                Commonness = Commonness.Half_year,
                Price = 3000
            },
                    new Injection()
            {
                Id = 3,
                Name = InjectionName.Canine_Hepatitis,
                Commonness = Commonness.Yearly,
                Price = 10000
            }
        }.AsQueryable());

            logic = new InjectionLogic(mockRepo.Object);
        }
        [Test]
        public void SumPrice()
        {
            var result = logic.SumPrice();

            Assert.That(result > 10000);
        }
    }
}
