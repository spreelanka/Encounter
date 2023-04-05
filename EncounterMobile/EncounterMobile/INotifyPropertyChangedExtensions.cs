using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EncounterMobile
{
    public static class INotifyPropertyChangedExtensions
    {
        public static bool SetProperty<T>(this INotifyPropertyChanged sender, ref T item, T value, OnPropertyChangedDelegate onPropertyChanged, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(item, value))
            {
                return false;
            }
            item = value;
            onPropertyChanged(propertyName);
            return true;
        }

        public delegate void OnPropertyChangedDelegate(string propertyName);
    }
}

