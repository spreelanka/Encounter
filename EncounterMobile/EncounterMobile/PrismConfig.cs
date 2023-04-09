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
using EncounterMobile.Helpers;
using Prism.Behaviors;
using Prism.Controls;
using Polly.Registry;
using EncounterMobile.NetworkPolicies;
using Polly;
using Microsoft.Extensions.Caching.Memory;
using Polly.Caching.Memory;
using NeoSmart.Caching.Sqlite;
using Polly.Caching.Distributed;

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


        // not unit testing this because:
        // https://github.com/App-vNext/Polly/wiki/Unit-testing-with-Polly---with-examples#4-i-want-to-unit-test-that-the-polly-policies-i-apply-actually-do-what-they-say-on-the-tin
        private static IAsyncPolicy DefaultPolicy()
        {
            var retry = Policy
                .Handle<HttpRequestException>()
                .RetryAsync(3);

            var breaker = Policy
                .Handle<HttpRequestException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromMinutes(1)
                );

            var cache = new MemoryCache(new MemoryCacheOptions());
            var cacheProvider = new MemoryCacheProvider(cache);
            
            var cachePolicy = Policy
                .CacheAsync(cacheProvider, TimeSpan.FromHours(1));

            var all = Policy.WrapAsync(cachePolicy, retry, breaker);
            return all;
        }

        private static PolicyRegistry policyRegistry;
        private static PolicyRegistry PolicyRegistry {
            get {
                if (policyRegistry != null)
                    return policyRegistry;
                policyRegistry = new PolicyRegistry
                {
                    { PolicyNames.DefaultPolicy, DefaultPolicy() }
                };
                return policyRegistry;
            }
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
                .RegisterInstance<RandomSeed>(null)
                .RegisterInstance<IReadOnlyPolicyRegistry<string>>(PolicyRegistry)

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
