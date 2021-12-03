using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheWeather.Helpers
{
    public class StaticResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var resourceKey = (string)value;
            return Application.Current.Resources[resourceKey];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
