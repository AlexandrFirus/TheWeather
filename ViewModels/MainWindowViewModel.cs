using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWeather.DataModels.WeatherData;
using TheWeather.Helpers;
using TheWeather.Models;

namespace TheWeather.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly MainWindowModel _model;
        private string _searchData;
        private IEnumerable<City> _foundedCities;
        private City _selectedCity;
        private bool _searchToggle;
        private string _lastUpdate;
        private Command _refreshCommand;
        private Command _loadedCommand;
        private Command _closingCommand;
        private Command _selectedCityChanged;

        public Command LoadedCommand => _loadedCommand ?? (_loadedCommand = new Command(LoadedCommandExecute));
        public Command ClosingCommand => _closingCommand ?? (_closingCommand = new Command(ClosingCommandExeute));
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(RefreshCommandExecute));
        public Command SelectedCityChanged => _selectedCityChanged ?? (_selectedCityChanged = new Command(SelectedCityChangedExecute));

        public string LastUpdate
        {
            get => _lastUpdate;
            set
            {
                _lastUpdate = value;
                RaisePropertyChanged(nameof(LastUpdate));
            }
        }

        public bool SearchToggle
        {
            get => _searchToggle;
            set
            {
                _searchToggle = value;
                RaisePropertyChanged(nameof(SearchToggle));
            }
        }

        public IEnumerable<City> FoundedCities
        {
            get => _foundedCities;
            set
            {
                _foundedCities = value;
                RaisePropertyChanged(nameof(FoundedCities));
            }
        }

        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (value != null)
                {
                    _selectedCity = value;
                    RaisePropertyChanged(nameof(SelectedCity));
                }
            }
        }

        public string SearchData
        {
            get => _searchData;
            set
            {
                _searchData = value;
                RaisePropertyChanged(nameof(SearchData));
                _ = SearchAsync(_searchData);
            }
        }

        public MainWindowViewModel(MainWindowModel mainWindowModel)
        {
            _model = mainWindowModel;
        }

        private async void SelectedCityChangedExecute(object parameter)
        {
            if (parameter is City city)
            {
                await UpdateCityInfoAsync(city.id);
            }
        }

        private async void RefreshCommandExecute()
        {
            await UpdateCityInfoAsync(SelectedCity.id);
        }

        private async void LoadedCommandExecute()
        {
            await UpdateCityInfoAsync(_model.DefaultCity);
        }

        private async Task UpdateCityInfoAsync(int newCityId)
        {
            try
            {
                var city = await _model.GetCityByIdAsync(newCityId);
                _model.DefaultCity = city.id;
                LastUpdate = TimeStampHelper.UnixTimeStampToDateTime(city?.dt).ToString();
                SelectedCity = city;
                SearchToggle = false;
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
            }
        }

        private async Task SearchAsync(string searchData)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(searchData))
                {
                    var result = await _model.GetCitiesAsync(searchData);
                    if (Enumerable.Count(result) > 0)
                    {
                        FoundedCities = result;
                    }
                }
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex);
            }
        }

        private void ClosingCommandExeute()
        {
            _model.SaveSettings();
            App.Current.Shutdown();
        }
    }
}