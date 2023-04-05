using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EncounterMobile
{
	public class BaseObservable: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

