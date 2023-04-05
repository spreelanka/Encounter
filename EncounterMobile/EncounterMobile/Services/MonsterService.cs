using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EncounterMobile.Models;
using EncounterMobile.Helpers;
using EncounterMobile.Services.Interfaces;

namespace EncounterMobile.Services
{
    public class MonsterService : BaseOpen5eService, IMonsterService
    {
        private string monsterPath = "monsters/?challenge_rating={0}";
        private int seed => constantSeed?.Seed ?? Environment.TickCount;

        private RandomSeed constantSeed = null;
        //private RandomSeed constantSeed = new RandomSeed { Seed = 1234 };


        public MonsterService(HttpMessageHandler messageHandler, RandomSeed seed = null) : base(messageHandler)
        {
            constantSeed = seed;
        }

        public async Task<Monster> GetMonster(int cr = 1)
        {
            var monsters = await GetMonsters(cr);
            var i = (new Random(seed)).Next(monsters.Count);
            return monsters[i];
        }

        public async Task<List<Monster>> GetMonsters(int cr = 1)
        {
            var relativePath = string.Format(monsterPath, cr);
            return (await Get<MonstersResponse>(relativePath)).results;
        }
    }
}

