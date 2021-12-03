using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TheWeather.Converters
{
    public class StringEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string val)
            {
                return string.IsNullOrWhiteSpace(val) ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
