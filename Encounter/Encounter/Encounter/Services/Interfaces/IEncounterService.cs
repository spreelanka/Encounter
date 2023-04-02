using System;
using Encounter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Encounter.Models;

namespace Encounter.Services.Interfaces
{
	public interface IEncounterService
	{
        Task<Models.Encounter> GetEncounter(int cr = 1);
    }
}

