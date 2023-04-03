using System;
using EncounterMobile.Services.Interfaces;

namespace EncounterMobile.ViewModels
{
	public class MainPageViewModel
	{
        protected IEncounterService encounterService { get; set; }
        public MainPageViewModel(IEncounterService encounterService)
        {
            Task.Run(async () =>
            {
                var encounter = await encounterService.GetEncounter();
                var monster = encounter.Monsters.First();

            });
        }
	}
}

