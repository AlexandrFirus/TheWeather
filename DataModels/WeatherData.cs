using System.Collections.Generic;

namespace TheWeather.DataModels.WeatherData
{
    public class Coord
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }

        public int Temp { get => (int)temp; set { } }
        public int FeelsLike { get => (int)feels_like; set { } }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Sys
    {
        public string country { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public Main main { get; set; }
        public int dt { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public object rain { get; set; }
        public object snow { get; set; }
        public Clouds clouds { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class WeatherRoot
    {
        public string message { get; set; }
        public string cod { get; set; }
        public int count { get; set; }
        public List<City> list { get; set; }
    }
}