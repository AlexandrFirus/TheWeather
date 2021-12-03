using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TheWeather.Converters
{
    public class FlagCodeToPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bitmapImage = new BitmapImage();
            if (value is string flagCode)
            {
                var url = $"https://flagcdn.com/16x12/{flagCode.ToLower()}.png";
                using var client = new WebClient();
                var bytes = client.DownloadData(url);
                using var stream = new MemoryStream(bytes);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
