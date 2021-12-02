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
    class OwnerLogicTest
    {
        IOwnerLogic logic;

        [SetUp]
        public void Setup()
        {
            Mock<IOwnerRepository> mockRepo = 
                new Mock<IOwnerRepository>();

            mockRepo
                .Setup(x => x.ReadAll())
                .Returns(new List<Owner>
                {
                    new Owner()
                    {
                        Id = 1,
                        Name = "Kristóf",
                        IdentityCardNumber = "ASD123",
                        Sex = Sex.Male
                    },
                    new Owner()
                    {
                        Id = 2,
                        Name = "Dominik",
                        IdentityCardNumber = "ASD321",
                        Sex = Sex.Male
                    },
                    new Owner()
                    {
                        Id = 3,
                        Name = "Eszter",
                        IdentityCardNumber = "DSA321",
                        Sex = Sex.Female
                    }
                }.AsQueryable());

            logic = new OwnerLogic(mockRepo.Object);
        }
        [Test]
        public void CountDogs()
        {
            IEnumerable<KeyValuePair<string, int>> result = logic.CountDogs();

            Assert.That(result.Any);
        }
    }
}
