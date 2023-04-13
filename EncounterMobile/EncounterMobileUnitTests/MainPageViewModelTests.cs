using System;
using EncounterMobile.Helpers;
using EncounterMobile.Models;
using EncounterMobile.Services.Interfaces;
using EncounterMobile.ViewModels;
using EncounterMobile.Views;
using Moq;
using Newtonsoft.Json;
using Prism.Navigation;

namespace EncounterMobileUnitTests
{
	public class MainPageViewModelTests
	{

        RandomSeed Seed = new RandomSeed { Seed = 1234 };
        Queue<int> ExpectedTileIndexes;

        Queue<string> MockEncounterJson;

        Mock<INavigationService> navigationServiceMoq;
        Mock<IEncounterService> encounterServiceMoq;

        MainPageViewModel subject;
        [SetUp]
        public async Task Setup()
        {
            ExpectedTileIndexes = new Queue<int>(new List<int> { 17, 38, 14, 40, 15, 40, 34, 22, 17, 38, 14, 40, 15, 40, 34, 22 });
            MockEncounterJson = new Queue<string>(new List<string> { "{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Demon, Inciter\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Broodiken\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Brown Bear\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Dark Servant\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Catterball\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Clockwork Soldier\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Broodiken\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Death Worm\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Carbuncle\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Cueyatl Sea Priest\"}]}",
            "{\"CR\":1,\"Monsters\":[{\"name\":\"Conniption Bug\"}]}"});

            navigationServiceMoq = new Mock<INavigationService>();
            navigationServiceMoq
                .Setup(c => c.NavigateAsync(It.IsAny<Uri>(), It.IsAny<INavigationParameters>()))
                .Returns((Uri uri, INavigationParameters navigationParameters) =>
                {
                    var rawUri = JsonConvert.SerializeObject(uri);
                    var rawNav = JsonConvert.SerializeObject(navigationParameters);
                    return Task.FromResult<INavigationResult>(null);
                }).Verifiable();
            encounterServiceMoq = new Mock<IEncounterService>();
            encounterServiceMoq
                .Setup(c => c.GetEncounter(It.IsAny<int>()))
                .Returns((int CR) => Task.FromResult(JsonConvert.DeserializeObject<Encounter>(MockEncounterJson.Dequeue())))
                .Verifiable();

            subject = new MainPageViewModel(navigationServiceMoq.Object,encounterServiceMoq.Object,Seed);
        }

        private string GetJson()
        {
            return "{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]}";
        }

        private string GetMapTilesJson1() => "[{\"Count\":0,\"MonsterName\":\"Bugbear\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/17.png\",\"Defeated\":false},{\"Count\":1,\"MonsterName\":\"Demon, Inciter\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Demon, Inciter\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/38.png\",\"Defeated\":false},{\"Count\":2,\"MonsterName\":\"Broodiken\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Broodiken\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/14.png\",\"Defeated\":false},{\"Count\":3,\"MonsterName\":\"Dire Wolf\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/40.png\",\"Defeated\":false},{\"Count\":4,\"MonsterName\":\"Brown Bear\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Brown Bear\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/15.png\",\"Defeated\":false},{\"Count\":5,\"MonsterName\":\"Dire Wolf\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/40.png\",\"Defeated\":false},{\"Count\":6,\"MonsterName\":\"Dark Servant\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dark Servant\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/34.png\",\"Defeated\":false},{\"Count\":7,\"MonsterName\":\"Catterball\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Catterball\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/22.png\",\"Defeated\":false}]";

        private string GetMapTilesJson2() => "[{\"Count\":8,\"MonsterName\":\"Clockwork Soldier\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Clockwork Soldier\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/28.png\",\"Defeated\":false},{\"Count\":9,\"MonsterName\":\"Broodiken\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Broodiken\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/14.png\",\"Defeated\":false},{\"Count\":10,\"MonsterName\":\"Bugbear\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/18.png\",\"Defeated\":false},{\"Count\":11,\"MonsterName\":\"Death Worm\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Death Worm\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/37.png\",\"Defeated\":false},{\"Count\":12,\"MonsterName\":\"Dire Wolf\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/40.png\",\"Defeated\":false},{\"Count\":13,\"MonsterName\":\"Carbuncle\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Carbuncle\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/20.png\",\"Defeated\":false},{\"Count\":14,\"MonsterName\":\"Cueyatl Sea Priest\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Cueyatl Sea Priest\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/32.png\",\"Defeated\":false},{\"Count\":15,\"MonsterName\":\"Conniption Bug\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Conniption Bug\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/29.png\",\"Defeated\":false}]";

        [Test]
        public async Task OnNavigatedTo_ClearsSelectedMapTile()
        {
            var navigationParams = new NavigationParameters();

            subject.OnNavigatedTo(navigationParams);
            Assert.IsNull(subject.SelectedMapTile);
        }

        [Test]
        public async Task GetRandomMapTiles_GetsExpected()
        {
            var expected1 = "[{\"Count\":0,\"MonsterName\":\"Bugbear\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/17.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Demon, Inciter\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Demon, Inciter\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/38.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Broodiken\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Broodiken\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/14.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Dire Wolf\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/40.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Brown Bear\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Brown Bear\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/15.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Dire Wolf\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/40.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Dark Servant\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dark Servant\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/34.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Catterball\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Catterball\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/22.png\",\"Defeated\":false}]";
            var mapTiles1 = await subject.GetRandomMapTiles(8);
            Assert.AreEqual(expected1, JsonConvert.SerializeObject(mapTiles1));

            var expected2 = "[{\"Count\":0,\"MonsterName\":\"Clockwork Soldier\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Clockwork Soldier\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/28.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Broodiken\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Broodiken\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/14.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Bugbear\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Bugbear\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/18.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Death Worm\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Death Worm\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/37.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Dire Wolf\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Dire Wolf\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/40.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Carbuncle\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Carbuncle\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/20.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Cueyatl Sea Priest\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Cueyatl Sea Priest\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/32.png\",\"Defeated\":false},{\"Count\":0,\"MonsterName\":\"Conniption Bug\",\"Encounter\":{\"CR\":1,\"Monsters\":[{\"name\":\"Conniption Bug\"}]},\"MapUri\":\"https://encounterstorage1.blob.core.windows.net/geomorphs/29.png\",\"Defeated\":false}]";
            var mapTiles2 = await subject.GetRandomMapTiles(8);
            Assert.AreEqual(expected2, JsonConvert.SerializeObject(mapTiles2));
        }

        [Test]
        public async Task OnNavigatedTo_callsLoadMore()
        {
            var navigationParams = new NavigationParameters();
            var collectionChangedNotified = false;
            subject.MapTiles.CollectionChanged += (o, e) => {
                collectionChangedNotified = true;
            };

            Assert.AreEqual(0, subject.MapTiles.Count);
            subject.OnNavigatedTo(navigationParams);
            Assert.AreEqual(8, subject.MapTiles.Count);

            Assert.IsTrue(collectionChangedNotified);
            Assert.IsNull(subject.SelectedMapTile);
        }

        [Test]
        public async Task LoadMore_UpdatesMapTiles()
        {
            var navigationParams = new NavigationParameters();

            var collectionChangedNotified = false;
            subject.MapTiles.CollectionChanged += (o, e) => {
                collectionChangedNotified = true;
            };

            subject.OnNavigatedTo(navigationParams);

            subject.LoadMore.Execute(null);
            Assert.IsTrue(collectionChangedNotified);

            var actual = JsonConvert.SerializeObject(subject.MapTiles);
            var expectedList = JsonConvert.DeserializeObject<List<MapTile>>(GetMapTilesJson1());
            expectedList.AddRange(JsonConvert.DeserializeObject<List<MapTile>>(GetMapTilesJson2()));
            var expected = JsonConvert.SerializeObject(expectedList);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task LoadMore_respectsLoadingDataFlag()
        {
            var navigationParams = new NavigationParameters();
            var expectedChangeCount = 8;

            var tcs = new TaskCompletionSource<bool>();
            var collectionChangedNotifiedCount = 0;
            subject.MapTiles.CollectionChanged += async (o, e) => {
                collectionChangedNotifiedCount += 1;
                if (collectionChangedNotifiedCount >= expectedChangeCount)
                {
                    await Task.Delay(100);
                    tcs.TrySetResult(true);
                }
            };
#pragma warning disable 4014
            Task.Run(async () =>
            {
                await Task.Delay(300);
                if(!tcs.Task.IsCompleted)
                    Assert.Fail("CollectionChanged timeout");
                tcs.TrySetCanceled();
            });
#pragma warning restore 4014

            await Task.WhenAll(
                Task.Run(async () => {
                    await Task.Delay(1);
                    subject.LoadMore.Execute(null);
                }),
                Task.Run(async () => {
                    await Task.Delay(0);
                    subject.LoadMore.Execute(null);
                }),
                tcs.Task
                );

            Assert.AreEqual(expectedChangeCount, collectionChangedNotifiedCount);

            var actual = JsonConvert.SerializeObject(subject.MapTiles);
            var expectedList = JsonConvert.DeserializeObject<List<MapTile>>(GetMapTilesJson1());
            var expected = JsonConvert.SerializeObject(expectedList);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task LoadMore_AppendsToMapTiles()
        {
            var navigationParams = new NavigationParameters();
            var expectedChangeCount = 16;

            var tcs = new TaskCompletionSource<bool>();
            var collectionChangedNotifiedCount = 0;
            subject.MapTiles.CollectionChanged += async (o, e) => {
                collectionChangedNotifiedCount += 1;
            };

            subject.LoadMore.Execute(null);
            subject.LoadMore.Execute(null);


            Assert.AreEqual(expectedChangeCount, collectionChangedNotifiedCount);

            var actual = JsonConvert.SerializeObject(subject.MapTiles);
            var expectedList = JsonConvert.DeserializeObject<List<MapTile>>(GetMapTilesJson1());
            expectedList.AddRange(JsonConvert.DeserializeObject<List<MapTile>>(GetMapTilesJson2()));
            var expected = JsonConvert.SerializeObject(expectedList);


            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task SelectedMapTileChangedCommand_nullSelectionDoesNotNavigate()
        {
            var navigationParams = new NavigationParameters();

            
            subject.OnNavigatedTo(navigationParams);
            subject.SelectedMapTile = null;
            subject.SelectedMapTileChangedCommand.Execute(null);
            Assert.IsNull(subject.SelectedMapTile);
            //navigationServiceMoq.Verify(e => e.NavigateAsync(It.Is<string>(n => n == nameof(MapTilePage)), parameters => true), Times.Never);
            navigationServiceMoq.Verify(e => e.NavigateAsync(It.IsAny<Uri>(), It.IsAny<INavigationParameters>()), Times.Never);
        }

        bool CompareNavigationParams(INavigationParameters expected, INavigationParameters actual)
        {
            var eJson = JsonConvert.SerializeObject(expected);
            var aJson = JsonConvert.SerializeObject(actual);
            return eJson == aJson;
        }

        [Test]
        public async Task SelectedMapTileChangedCommand_navigates()
        {
            subject.OnNavigatedTo(new NavigationParameters());
            var expectedSelectedMapTile = subject.MapTiles[0];
            subject.SelectedMapTile = expectedSelectedMapTile;
            subject.SelectedMapTileChangedCommand.Execute(null);
            var navigationParams = new NavigationParameters { { nameof(MapTile), expectedSelectedMapTile } };

            var expectedUriJson = $"\"{nameof(MapTilePage)}\"";
            Assert.AreEqual(JsonConvert.SerializeObject(expectedSelectedMapTile), JsonConvert.SerializeObject(subject.SelectedMapTile));

            //navigationServiceMoq.Verify(e => e.NavigateAsync(It.IsAny<Uri>(),It.IsAny<INavigationParameters>()),Times.AtLeastOnce);

            navigationServiceMoq.Verify(e => e.NavigateAsync(
                    It.Is<Uri>(u => JsonConvert.SerializeObject(u) == expectedUriJson),
                    It.Is<INavigationParameters>(p => CompareNavigationParams(navigationParams,p))
                ),
                Times.Once);
        }
    }
}

