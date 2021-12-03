using System.Windows;
using System.Windows.Controls;

namespace TheWeather.Helpers
{
    public class WeatherIconSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (container is FrameworkElement element)
            {
                if (item is string resourceKey)
                {
                    return element.FindResource(resourceKey) as DataTemplate;
                }
            }
            return null;
        }
    }
}
