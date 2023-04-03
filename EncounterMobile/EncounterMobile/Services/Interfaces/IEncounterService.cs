using System;
using EncounterMobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EncounterMobile.Services.Interfaces
{
	public interface IEncounterService
	{
        Task<Encounter> GetEncounter(int cr = 1);
    }
}

