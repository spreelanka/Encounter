﻿using System;
using Moq;
using Moq.Protected;
using System.Text;
using EncounterMobile.Services;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;
using Polly;
using Polly.Registry;
using EncounterMobile.NetworkPolicies;

namespace EncounterMobileUnitTests
{
	public class EncounterServiceTests
	{

        IReadOnlyPolicyRegistry<string> policyRegistry;
        [SetUp]
        public void Setup()
        {
            policyRegistry = new PolicyRegistry
            {
                { PolicyNames.DefaultPolicy, Policy.NoOpAsync() }
            };
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

            var subject = new EncounterService(monsterServiceMoq.Object, httpMessageHandlerMoq.Object, policyRegistry);
            var result = await subject.GetEncounter();
            Assert.AreEqual(1, result.CR);
            Assert.AreEqual(1, result.Monsters.Count);
            Assert.AreEqual("Bugbear", result.Monsters[0].name);
        }
    }
}

