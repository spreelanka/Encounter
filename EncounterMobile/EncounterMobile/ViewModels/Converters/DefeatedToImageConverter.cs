using System;
using System.Globalization;

namespace EncounterMobile.ViewModels.Converters
{
	public class DefeatedToImageConverter : IValueConverter
    {
        public object Convert(object defeated, Type targetType, object parameter, CultureInfo culture)
        {
            var r = ((bool)defeated) ? "goblin_dead.png" : "goblin.png";
            return r;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("two way binding not applicable. don't use.");
        }
    }
}

