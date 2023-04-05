using System;
using DryIoc;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using System.Net;
using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;
using EncounterMobile.ViewModels;
using EncounterMobile.Services.Interfaces;
using EncounterMobile.Services;
using EncounterMobile.Views;
using Prism.Behaviors;
using Prism.Controls;

namespace EncounterMobile
{
    internal static class PrismConfig
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                //.ConfigureLogging(logging => logging.AddConsole())
                .ConfigureModuleCatalog(ConfigureModuleCatalog)
                .OnInitialized(OnInitialized)
                .OnAppStart($"{nameof(PrismNavigationPage)}/{nameof(MainPage)}");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterForNavigation<PrismNavigationPage>()
                .RegisterForNavigation<MainPage, MainPageViewModel>()
                .RegisterForNavigation<MapTilePage, MapTilePageViewModel>()
                .RegisterSingleton<HttpMessageHandler, HttpClientHandler>()
                .RegisterSingleton<IMonsterService, MonsterService>()
                .RegisterSingleton<IEncounterService, EncounterService>()

                //.RegisterSingleton<IAppInfo, AppInfoImplementation>()
                //.RegisterScoped<BaseServices>()
                //.RegisterSingleton
                .RegisterPageBehavior<NavigationPage,NavigationPageSystemGoBackBehavior>()
                .RegisterInstance(SemanticScreenReader.Default)
                .RegisterInstance(DeviceInfo.Current)
                .RegisterInstance(Launcher.Default);
        }

        private static void OnInitialized(IContainerProvider container)
        {
        }

        private static void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog
            //    .AddModule<AuthenticationModule>()
            //    .AddModule<CoreAppModule>(InitializationMode.OnDemand);
        }
    }
}
