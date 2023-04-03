using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;

namespace EncounterMobile.Services
{
	public class MonsterService: BaseOpen5eService, IMonsterService
	{
        private string monsterPath = "monsters/?challenge_rating={0}";

        public MonsterService(HttpMessageHandler messageHandler) : base(messageHandler)
        {
        }

        public async Task<Monster> GetMonster(int cr = 1)
        {
            var monsters = await GetMonsters(cr);
            var i = (new Random()).Next(monsters.Count);
            return monsters[i];
        }

        public async Task<List<Monster>> GetMonsters(int cr = 1)
        {
            var relativePath = string.Format(monsterPath, cr);
            return (await Get<MonstersResponse>(relativePath)).results;
        }
    }
}

