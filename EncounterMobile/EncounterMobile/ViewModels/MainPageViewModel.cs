using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;
using EncounterMobile.Views;
using Prism.Navigation.Xaml;
using static Java.Util.Jar.Attributes;

namespace EncounterMobile.ViewModels
{
	public class MainPageViewModel: BaseViewModel
	{
        protected IEncounterService encounterService { get; set; }
        
        ObservableCollection<MapTile> mapTiles;
        public ObservableCollection<MapTile> MapTiles { 
            get => mapTiles;
            set => this.SetProperty(ref mapTiles, value, OnPropertyChanged);
        }

        MapTile selectedMapTile;
        public MapTile SelectedMapTile
        {
            get => selectedMapTile;
            set => this.SetProperty(ref selectedMapTile, value, OnPropertyChanged);
        }

        public MainPageViewModel(INavigationService navigationService, IEncounterService encounterService):base(navigationService)
        {
            this.encounterService = encounterService;
            Task.Run(async () =>
            {
                var mapTiles = new ObservableCollection<MapTile>();
                for (var i = 0; i < 9; i++)
                {
                    var encounter = await encounterService.GetEncounter();
                    var t = new MapTile { Encounter = encounter , MapUri = new Uri($"https://encounterstorage1.blob.core.windows.net/geomorphs/{i%6+1}.png")};
                    mapTiles.Add(t);
                }
                MapTiles = mapTiles;
            });
        }

        public Command SelectedMapTileChangedCommand => new Command((sender) =>
        {
            if(SelectedMapTile!=null)
                navigationService.NavigateAsync(nameof(MapTilePage),new NavigationParameters { {nameof(MapTile) , SelectedMapTile } });
        });

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SelectedMapTile = null;
        }
    }
}

