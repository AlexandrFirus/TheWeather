using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TheWeather.DataModels.WeatherData;
using TheWeather.Interfaces;

namespace TheWeather.Services
{
    public class OpenWeatherMap : IWeatherService
    {
        private readonly string API_KEY = "31e9f5500a70ce19e013df137c0e30f6";
        private readonly string API_URL = "https://api.openweathermap.org/data/2.5";
        private string API_ADDED_PARAMETERS => $"&cnt=50&type=like&units=metric&appid={API_KEY}";
        readonly HttpClient client = new HttpClient();

        public async Task<City> GetCityByIdAsync(int id)
        {
            string method = $"weather?id={id}";
            var json = await GetJson(method);
            var result = JsonConvert.DeserializeObject<City>(json);
            return result;
        }

        public async Task<IEnumerable<City>> GetMatchedCitiesAsync(string parameter)
        {
            var url = $"find?q={parameter}";
            var json = await GetJson(url);
            var result = JsonConvert.DeserializeObject<WeatherRoot>(json).list;
            return result;
        }

        private async Task<string> GetJson(string method)
        {
            var url = $"{API_URL}/{method}{API_ADDED_PARAMETERS}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
