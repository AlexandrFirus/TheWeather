﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TheWeather.Interfaces;
using TheWeather.Models;
using TheWeather.ViewModels;

namespace TheWeather
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _provider;

        protected override void OnStartup(StartupEventArgs e)
        {
            BuildServiceProvider();
            ShowMainWindow();
            base.OnStartup(e);
        }

        private void ShowMainWindow()
        {
            var mainWindow = _provider.GetRequiredService<MainWindow>();
            mainWindow.Show();            
        }

        private void BuildServiceProvider()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IWeatherService, OpenWeatherMap>();
            collection.AddSingleton<ISettingsLoader, AppSettingsLoader>();
            collection.AddSingleton(provider => new MainWindowModel(provider.GetRequiredService<IWeatherService>(), provider.GetRequiredService<ISettingsLoader>()));
            collection.AddSingleton(provider => new MainWindowViewModel(provider.GetRequiredService<MainWindowModel>()));
            collection.AddSingleton(provider => new MainWindow { DataContext = provider.GetRequiredService<MainWindowViewModel>() });
            _provider = collection.BuildServiceProvider();
        }
    }
}