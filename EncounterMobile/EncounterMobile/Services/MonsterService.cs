using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EncounterMobile.Models;
using EncounterMobile.Helpers;
using EncounterMobile.Services.Interfaces;
using Polly.Registry;
using Newtonsoft.Json;

namespace EncounterMobile.Services
{
    public class MonsterService : BaseOpen5eService, IMonsterService
    {
        private string monsterPath = "monsters/?challenge_rating={0}";
        private int seed => constantSeed?.Seed ?? Environment.TickCount;

        RandomSeed constantSeed = null;
        readonly Random random;

        public MonsterService(HttpMessageHandler messageHandler, IReadOnlyPolicyRegistry<string> policyRegistry, RandomSeed seed = null) : base(messageHandler, policyRegistry)
        {
            constantSeed = seed;
            random = new Random(this.seed);
        }

        public async Task<Monster> GetMonster(int cr = 1)
        {
            var monsters = await GetMonsters(cr);
            var i = random.Next(monsters.Count);
            return monsters[i];
        }

        public async Task<List<Monster>> GetMonsters(int cr = 1)
        {
            var relativePath = string.Format(monsterPath, cr);
            var v = (await Get<MonstersResponse>(relativePath)).results;
            var raw = JsonConvert.SerializeObject(v);
            return v;
        }
    }
}

