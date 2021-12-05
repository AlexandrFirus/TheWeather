using System;
using System.Diagnostics;
using TheWeather.Interfaces;

namespace TheWeather.Services
{
    public class Logger : ILogger
    {
        public void Exception(Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }
    }
}
