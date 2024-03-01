using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Windows.Data;
using WindService.Interfaces;

namespace TheWeather.Converters
{
    public class WindSpeedToStrengthConverter : IValueConverter
    {
        private static readonly IWindSpeedService service = App.ServiceProvider.GetRequiredService<IWindSpeedService>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double windSpeed)
                return service.SpeedDescription(windSpeed);
            throw new ArgumentException("Windspeed value must be a double");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
