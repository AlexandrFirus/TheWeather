using System.Collections.Generic;
using System.Threading.Tasks;
using TheWeather.DataModels;
using TheWeather.DataModels.WeatherData;
using TheWeather.Interfaces;

namespace TheWeather.Models
{
    public class MainWindowModel
    {
        private readonly IWeatherService _weatherService;
        private readonly ISettingsLoader _settingsLoader;

        public MainWindowModel(IWeatherService weatherService, ISettingsLoader settingsLoader)
        {
            _weatherService = weatherService;
            _settingsLoader = settingsLoader;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync(string searchParameter) => await _weatherService.GetMatchedCitiesAsync(searchParameter);

        public async Task<City> GetCityByIdAsync(int id) => await _weatherService.GetCityByIdAsync(id);

        public AppSettings Settings => _settingsLoader.Settings;

        public void SaveSettings() => _settingsLoader.Save();
    }
}
