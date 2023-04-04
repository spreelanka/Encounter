using System;
using Moq;
using Moq.Protected;
using System.Text;
using EncounterMobile.Services;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;

namespace EncounterMobileUnitTests
{
	public class EncounterServiceTests
	{
        //private MonsterService.RandomSeed Seed = new MonsterService.RandomSeed { Seed = 1234 };
        //private int ExpectedRandomIndexFromSeed = 19;

        [SetUp]
        public void Setup()
        {

        }

        private Task<Monster> GetMonster_MockResponse(int CR)
        {
            var response = new Monster { name = "Bugbear" };
            return Task.FromResult(response);
        }

        [Test]
        public async Task GetEncounter_ReturnsExpected()
        {
            var httpMessageHandlerMoq = new Mock<HttpMessageHandler>();

            var monsterServiceMoq = new Mock<IMonsterService>();
            monsterServiceMoq
                .Setup(c => c.GetMonster(It.IsAny<int>()))
                .Returns((int CR) => GetMonster_MockResponse(CR))
                .Verifiable();

            var subject = new EncounterService(monsterServiceMoq.Object, httpMessageHandlerMoq.Object);
            var result = await subject.GetEncounter();
            Assert.AreEqual(1, result.CR);
            Assert.AreEqual(1, result.Monsters.Count);
            Assert.AreEqual("Bugbear", result.Monsters[0].name);
        }
    }
}

