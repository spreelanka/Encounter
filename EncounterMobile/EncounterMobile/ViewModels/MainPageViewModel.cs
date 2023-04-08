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
using System.Windows.Input;

namespace EncounterMobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        const int UniqueGeomorphCount = 42;
        protected IEncounterService encounterService { get; set; }
        private int seed => constantSeed?.Seed ?? Environment.TickCount;
        private RandomSeed constantSeed = null;

        int loadMoreThreshold = 10;
        public int LoadMoreThreshold
        {

            get => loadMoreThreshold;
            set => this.SetProperty(ref loadMoreThreshold, value, OnPropertyChanged);
        }

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

        public MainPageViewModel(INavigationService navigationService, IEncounterService encounterService, RandomSeed seed = null) : base(navigationService)
        {
            //ItemsLayout = "VerticalGrid, 2"
            this.constantSeed = seed;
            this.encounterService = encounterService;
            MapTiles = new ObservableCollection<MapTile>();
            //Task.Run(async () =>
            //{
            //    var mapTiles = new ObservableCollection<MapTile>();
            //    foreach (var m in await GetRandomMapTiles(10))
            //        mapTiles.Add(m);

            //    MapTiles = mapTiles;
            //    //await Task.Delay(2000);
            //    //RemainingItemsThreshold = 10;
            //});
        }

        public async Task<IEnumerable<MapTile>> GetRandomMapTiles(int number)
        {
            var mapTiles = new ObservableCollection<MapTile>();
            for (var i = 0; i < number; i++)
            {
                var encounter = await encounterService.GetEncounter();
                var tileIndex = (new Random(seed)).Next(UniqueGeomorphCount) + 1;
                var t = new MapTile { Encounter = encounter, MapUri = new Uri($"https://encounterstorage1.blob.core.windows.net/geomorphs/{tileIndex}.png") };
                mapTiles.Add(t);
            }
            return mapTiles;
        }

        public Command SelectedMapTileChangedCommand => new Command((sender) =>
        {
            if (SelectedMapTile != null)
                navigationService.NavigateAsync(nameof(MapTilePage), new NavigationParameters { { nameof(MapTile), SelectedMapTile } });
        });

        bool debugFlag;
        private bool loadingData;
        public ICommand LoadMore => new Command(async () =>
        {
            if (!loadingData)// && MapTiles?.Count > 0)
            {
                loadingData = true;
                var mapTiles = new ObservableCollection<MapTile>(MapTiles);
                var newMapTiles = await GetRandomMapTiles(1);
                MapTiles.Add(newMapTiles.FirstOrDefault());
                loadingData = false;
                //Task.Run(async () =>
                //{
                    
                //    MainThread.InvokeOnMainThreadAsync(() =>
                //    {
                //        //foreach (var m in newMapTiles)
                //        //    mapTiles.Add(m);
                //        //if (!debugFlag) {
                //        //    debugFlag = true;
                //        //    MapTiles = mapTiles;
                //        //}

                //        //OnPropertyChanged(nameof(MapTiles));
                //        //this.SetProperty(ref mapTiles, MapTiles, OnPropertyChanged, nameof(MapTiles));
                //        //loadingData = false;
                //    });
                //});
            }
        });

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            SelectedMapTile = null;
            LoadMore.Execute(null);
        }
    }
}

