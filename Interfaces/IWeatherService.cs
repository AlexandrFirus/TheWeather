using System.Collections.Generic;
using System.Threading.Tasks;
using TheWeather.DataModels.WeatherData;

namespace TheWeather.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<City>> GetMatchedCitiesAsync(string searchedData);
        Task<City> GetCityByIdAsync(int id);
    }
}