using TheWeather.DataModels;

namespace TheWeather.Interfaces
{
    public interface ISettingsLoader
    {
        AppSettings Settings { get; }
        void Save();
    }
}
