using System;
using Encounter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Encounter.Services.Interfaces
{
	public interface IMonsterService
	{
        Task<List<Monster>> GetMonsters(int cr = 1);
        Task<Monster> GetMonster(int cr = 1);
    }
}

