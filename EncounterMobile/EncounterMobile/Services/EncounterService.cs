	using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EncounterMobile.Services.Interfaces;
using Newtonsoft.Json;
using Polly.Registry;

namespace EncounterMobile.Services
{
	public class EncounterService: BaseOpen5eService, IEncounterService
	{
		protected IMonsterService monsterService;
        public EncounterService(IMonsterService monsterService, HttpMessageHandler messageHandler, IReadOnlyPolicyRegistry<string> policyRegistry) : base(messageHandler, policyRegistry)
        {
			this.monsterService = monsterService;
		}

        public async Task<Models.Encounter> GetEncounter(int cr = 1)
        {
			var monster = await monsterService.GetMonster(cr);
			var encounter = new Models.Encounter { CR = cr, Monsters = new List<Models.Monster> { monster } };

            return encounter;
        }
    }
}

