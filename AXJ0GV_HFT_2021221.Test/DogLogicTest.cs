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
                    Species = "Pug",
                    OwnerID = 1,
                    InjectionID = 1
                },
                new Dog()
                {
                    Id = 2,
                    Name = "Málna",
                    Sex = Sex.Female,
                    Species = "Pug",
                    OwnerID = 1
                },
                new Dog()
                {
                    Id = 3,
                    Name = "Sebi",
                    Sex = Sex.Female,
                    Species = "Dög",
                    OwnerID = 1,
                    InjectionID = 1
                }
        }.AsQueryable());
            logic = new DogLogic(mockRepo.Object);
        }
        [Test]
        public void TestReadAll()
        {
            var asd = logic.ReadAll().Count();
            Assert.That(asd == 3);
        }
        [Test]
        public void CountByOwner()
        {
            Owner Doma = new Owner()
            {
                Id = 1,
                Name = "Dominik",
                IdentityCardNumber = "ASD321",
                Sex = Sex.Male
            };
            int test = logic.CountByOwner(Doma.Id);

            Assert.That(test == 3);

        }
        [Test]
        public void GetUsedInjections()
        {
            var usedinjections = logic.GetUsedInjections();
            Assert.That(usedinjections.Count == 2);

        }
        [Test]
        public void CountByInjection()
        {
            Injection Bordetella = new Injection()
            {
                Id = 1,
                Name = InjectionName.Bordetella_Bronchiseptica,
                Commonness = Commonness.Once,
                Price = 1000
            };
            int test = logic.CountByInjection(Bordetella.Id);

            Assert.That(test == 2);

        }
        [Test]
        public void TestCreate()
        {
            Dog testDog = new Dog()
            {
                Id = 1,
                Name = "Áfi",
                Sex = Sex.Female,
            };

            Assert.Throws(typeof(Exception), () => logic.Create(testDog));
        }
    }
}
