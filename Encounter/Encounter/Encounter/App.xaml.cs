using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Encounter.Services;
using Encounter.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Prism.Plugin.Popups;
using Encounter.Services.Interfaces;
using System.Threading.Tasks;
using System.Net.Http;

namespace Encounter
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync(nameof(AboutPage));
        }

        protected override void OnStart()
        {
            base.OnStart();

            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // This updates INavigationService and registers PopupNavigation.Instance
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterSingleton<HttpMessageHandler, HttpClientHandler>();
            containerRegistry.RegisterSingleton<IMonsterService, MonsterService>();
            containerRegistry.RegisterSingleton<IEncounterService, EncounterService>();
            containerRegistry.RegisterForNavigation<AboutPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();
        }

        void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                //have network
            }
            else
            {
                //no network
            }
        }
    }
}

