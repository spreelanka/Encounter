using System;
using Prism.Common;

namespace EncounterMobile.Models
{
	public class MapTile:BindableBase
	{
        public int Count { get; set; }
		public string MonsterName => Encounter?.Monsters[0]?.name;
		public Encounter Encounter { get; set; }
		public Uri MapUri
		{ get;set;}
        //=> new Uri("https://encounterstorage1.blob.core.windows.net/geomorphs/2.png");

        bool defeated;
        public bool Defeated
        { 
            get => defeated;
            set => SetProperty(ref defeated, value);
        }
    }
}

