using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using EncounterMobile.Models;
using EncounterMobile.Services;
using EncounterMobile.Services.Interfaces;
using EncounterMobile.Views;
using EncounterMobile.Helpers;
using Prism.Navigation.Xaml;

namespace EncounterMobile.ViewModels
{
	public class MainPageViewModel: BaseViewModel
	{
        const int UniqueGeomorphCount = 42;
        protected IEncounterService encounterService { get; set; }
        private int seed => constantSeed?.Seed ?? Environment.TickCount;
        private RandomSeed constantSeed = null;


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

        public MainPageViewModel(INavigationService navigationService, IEncounterService encounterService, RandomSeed seed = null) :base(navigationService)
        {
            this.constantSeed = seed;
            this.encounterService = encounterService;
            Task.Run(async () =>
            {
                var mapTiles = new ObservableCollection<MapTile>();
                foreach(var m in await GetRandomMapTiles(10))
                    mapTiles.Add(m);

                MapTiles = mapTiles;
            });
        }

        public async Task<IEnumerable<MapTile>> GetRandomMapTiles(int number)
        {
            var mapTiles = new ObservableCollection<MapTile>();
            for (var i = 0; i < number; i++)
            {
                var encounter = await encounterService.GetEncounter();
                var tileIndex = (new Random(seed)).Next(UniqueGeomorphCount)+1;
                var t = new MapTile { Encounter = encounter, MapUri = new Uri($"https://encounterstorage1.blob.core.windows.net/geomorphs/{tileIndex}.png") };
                mapTiles.Add(t);
            }
            return mapTiles;
        }

        public Command SelectedMapTileChangedCommand => new Command((sender) =>
        {
            if(SelectedMapTile!=null)
                navigationService.NavigateAsync(nameof(MapTilePage),new NavigationParameters { {nameof(MapTile) , SelectedMapTile } });
        });

        public Command RemainingItemsThresholdReachedCommand => new Command((sender) =>
        {
            if (MapTiles?.Count > 0)
            {
                Task.Run(async () =>
                {
                    foreach (var m in await GetRandomMapTiles(20))
                        MapTiles.Add(m);
                });
            }
        });

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SelectedMapTile = null;
        }
    }
}

