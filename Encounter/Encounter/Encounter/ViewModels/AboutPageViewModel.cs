using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Encounter.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace Encounter.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {
        protected IEncounterService encounterService { get; set; }
        public AboutPageViewModel(IEncounterService encounterService)
        {
            this.encounterService = encounterService;

            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

            Task.Run(async () =>
            {
                var encounter = await encounterService.GetEncounter();
                var monster = encounter.Monsters.First();

            });
        }

        public ICommand OpenWebCommand { get; }
    }
}
