using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EncounterMobile.ViewModels
{
	public abstract class BaseViewModel: INotifyPropertyChanged, INavigationAware
	{
        protected INavigationService navigationService { get; set; }

        public BaseViewModel(INavigationService navigationService)
		{
			this.navigationService = navigationService;
		}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public virtual void OnNavigatedFrom(INavigationParameters parameters){}

        public virtual void OnNavigatedTo(INavigationParameters parameters){}
    }
}

