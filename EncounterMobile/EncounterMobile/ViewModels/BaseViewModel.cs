using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EncounterMobile.ViewModels
{
	public abstract class BaseViewModel: BaseObservable, INavigationAware
	{
        protected INavigationService navigationService { get; set; }

        public BaseViewModel(INavigationService navigationService)
		{
			this.navigationService = navigationService;
		}

        public virtual void OnNavigatedFrom(INavigationParameters parameters){}

        public virtual void OnNavigatedTo(INavigationParameters parameters){}
    }
}

