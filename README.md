# EncounterApp
A simple infinite scrolling d&d dungeon app to highlight some dotnet maui features and best practices. 

## Concept
infinite scrolling dungeon. each tile has 1 level 1 monster. tapping the tile will show the monster stat block. checkbox to defeat monster. icon will change from full color to b&w monster after defeat.
|  | |
| -- | -- |
|![](docs/clip1.gif) |![](docs/clip2.gif) |

## architecture and feature highlights 
- rest api architecture [PrismConfig.cs #L41](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/PrismConfig.cs#L41)
	- resilient
		- retry
		- circuit breaker
		- caching - configurable per request pattern. currently only one caching policy
	- parsing
		- deserialization to objects strategy [MonsterService.cs #L38](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/Services/MonsterService.cs#L38)
		- request/response classes with no additional properties or methods
- service oriented architecture/Direct Injection [PrismConfig.cs #L79](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/PrismConfig.cs#L79)
	- dependencies are constructor injected as needed [MainPageViewModel.cs #L38](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/ViewModels/MainPageViewModel.cs#L38)
- general/misc
	- input validation: validate input and throw relevant exceptions [MapTilPageViewModel #L39](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/ViewModels/MapTilePageViewModel.cs#L29)
	- typed string sources to prevent typos (ex: `$"{nameof(MapTilePage)}"`  not `"MapTilePage"`)
- MVVM UI
	- xaml
		- data binding
		- ui value converters (ex: bool to string resource uri to toggle image based on binding) [MainPage.xaml #L51](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/Views/MainPage.xaml#L51)
		- multibinding: string format data binding [MainPage.xaml #L25](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/Views/MainPage.xaml#L25)
	- infinite scrolling collectionview: new data is fetched on demand just in time [MainPage.xaml #L15](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/Views/MainPage.xaml#L15)
- unit testing
	- dependencies injected to allow for unit testing [MonsterService.cs #L21](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobile/Services/MonsterService.cs#L21)
		- RandomSeed injected to provide deterministic scenarios in test
		- HttpMessageHandler injected to test rest services without real api calls
		- Policy.NoOpAsync() so services are tested without retry/cache
	- mock protected
- UI Test/Integration testing [Tests.cs #L56](https://github.com/spreelanka/Encounter/blob/cc7a22c90af2aaf3df2139f8d17c3fff68688a15/EncounterMobile/EncounterMobileUITests/Tests.cs#L56)
	- automate full app deployment to test specific app use scenarios
