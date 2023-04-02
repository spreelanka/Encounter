	using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Encounter.Services.Interfaces;

namespace Encounter.Services
{
	public class EncounterService: BaseOpen5eService, IEncounterService
	{
		protected IMonsterService monsterService;
        public EncounterService(IMonsterService monsterService)
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

