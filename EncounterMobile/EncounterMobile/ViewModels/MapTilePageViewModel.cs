using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;
using Newtonsoft.Json;

namespace EncounterMobile.ViewModels
{
    public class MapTilePageViewModel : BaseViewModel
    {
        MapTile mapTile;
        public MapTile MapTile
        {
            get => mapTile;
            set => this.SetProperty(ref mapTile, value, OnPropertyChanged);
        }

        public Monster Monster => MapTile.Encounter.Monsters[0];
        public MapTilePageViewModel(INavigationService navigationService): base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            MapTile = parameters.FirstOrDefault(e => e.Key == nameof(MapTile)).Value as MapTile;
            if (MapTile == null)
                throw new ArgumentNullException("must provide MapTile in NavigationParameters");
        }
    }
}

