using Newtonsoft.Json;
using System.IO;
using TheWeather.DataModels;
using TheWeather.Interfaces;

namespace TheWeather
{
    public class AppSettingsLoader : ISettingsLoader
    {
        private readonly string SETTINGS_PATH = "settings";

        private AppSettings _settings;
        public AppSettings Settings => _settings ??= Load();

        private AppSettings Load()
        {
            if (File.Exists(SETTINGS_PATH))
            {
                var json = File.ReadAllText(SETTINGS_PATH);
                var settings = JsonConvert.DeserializeObject<AppSettings>(json);

                return settings ??= new AppSettings();
            }

            return new AppSettings();
        }

        public void Save()
        {
            File.WriteAllText(SETTINGS_PATH, JsonConvert.SerializeObject(_settings));
        }
    }
}
