using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;

namespace EncounterMobile.ViewModels
{
    public class MapTilePageViewModel : BaseViewModel
    {
        protected IEncounterService encounterService { get; set; }
        MapTile mapTile;

        public MapTile MapTile
        {
            get => mapTile;
            set => this.SetProperty(ref mapTile, value, OnPropertyChanged);
        }

        public Monster Monster => MapTile.Encounter.Monsters[0];
        public MapTilePageViewModel(INavigationService navigationService, IEncounterService encounterService): base(navigationService)
        {
            this.encounterService = encounterService;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            MapTile = parameters.FirstOrDefault(e => e.Key == nameof(MapTile)).Value as MapTile;
        }
    }
}

