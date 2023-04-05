using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;

namespace EncounterMobile.ViewModels
{
	public class MainPageViewModel: INotifyPropertyChanged
	{
        protected IEncounterService encounterService { get; set; }

        IEnumerable<MapTile> mapTiles;
        public IEnumerable<MapTile> MapTiles { 
            get => mapTiles;
            set => this.SetProperty(ref mapTiles, value, OnPropertyChanged);
        }

        public MainPageViewModel(IEncounterService encounterService)
        {
            Task.Run(async () =>
            {
                var mapTiles = new List<MapTile>();
                for (var i = 0; i < 9; i++)
                {
                    var encounter = await encounterService.GetEncounter();
                    var t = new MapTile { Encounter = encounter , MapUri = new Uri($"https://encounterstorage1.blob.core.windows.net/geomorphs/{i%6+1}.png")};
                    mapTiles.Add(t);
                }
                MapTiles = mapTiles;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

