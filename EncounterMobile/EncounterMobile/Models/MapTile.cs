using System;
namespace EncounterMobile.Models
{
	public class MapTile
	{
		public string MonsterName => Encounter?.Monsters[0]?.name;
		public Encounter Encounter { get; set; }
		public Uri MapUri
		{ get;set;}
			//=> new Uri("https://encounterstorage1.blob.core.windows.net/geomorphs/2.png");
		public bool Defeated { get; set; }
	}
}

