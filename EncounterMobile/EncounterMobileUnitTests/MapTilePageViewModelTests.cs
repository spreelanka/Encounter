using System;
using System.Collections;
using EncounterMobile.Models;
using EncounterMobile.ViewModels;
using Moq;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;
using Prism.Common;

namespace EncounterMobileUnitTests
{
	public class MapTilePageViewModelTests
	{
        MapTilePageViewModel subject;
        [SetUp]
        public void Setup()
        {
            var navigationServiceMoq = new Mock<INavigationService>();

            subject = new MapTilePageViewModel(navigationServiceMoq.Object);
        }
        private string GetJson()
        {
            return "{\"Count\":0,\"MonsterName\":\"Dark Servant\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dark Servant\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/35.png\",\"Defeated\":false}";
        }

        [Test]
        public async Task OnNavigatedTo_ParsesAndNotifies()
        {

            var paramsMock= new Mock<INavigationParameters>();

            var mapTile = JsonConvert.DeserializeObject<MapTile>(GetJson());
            var navigationParams = new NavigationParameters { { nameof(MapTile), mapTile } };
            var mapTilePropWasNotified = false;
            subject.PropertyChanged += (o, c) => {
                if(c.PropertyName==nameof(MapTilePageViewModel.MapTile))
                    mapTilePropWasNotified = true;
            };

            subject.OnNavigatedTo(navigationParams);
            Assert.IsTrue(mapTilePropWasNotified);
            Assert.AreEqual(JsonConvert.SerializeObject(mapTile), JsonConvert.SerializeObject(subject.MapTile));
        }

        [Test]
        public async Task OnNavigatedTo_ThrowsArgumentNull()
        {

            var paramsMock = new Mock<INavigationParameters>();

            var navigationParams = new NavigationParameters();
            var mapTilePropWasNotified = false;
            subject.PropertyChanged += (o, c) => {
                if (c.PropertyName == nameof(MapTilePageViewModel.MapTile))
                    mapTilePropWasNotified = true;
            };
            Assert.Throws<ArgumentNullException>(() => subject.OnNavigatedTo(navigationParams));
            Assert.IsFalse(mapTilePropWasNotified);
        }
    }
}

