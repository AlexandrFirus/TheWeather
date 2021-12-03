using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Command LoadedCommand => _loadedCommand ?? (_loadedCommand = new Command(LoadedCommandExecute));
        public Command ClosingCommand => _closingCommand ?? (_closingCommand = new Command(ClosingCommandExeute));
        public Command RefreshCommand => _refreshCommand ?? (_refreshCommand = new Command(RefreshCommandExecute));

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
                    UpdateInfo();
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
            catch { }
        }

        private void UpdateInfo()
        {
            SearchToggle = false;
            _model.DefaultCity = SelectedCity.id;
            LastUpdate = TimeStampHelper.UnixTimeStampToDateTime(SelectedCity?.dt).ToString();
            FoundedCities = null;
        }

        private async void RefreshCommandExecute(object _)
        {
            SelectedCity = await _model.GetCityByIdAsync(SelectedCity.id);
        }

        private async void LoadedCommandExecute(object _)
        {
            try
            {
                SelectedCity = await _model.GetCityByIdAsync(_model.DefaultCity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        private void ClosingCommandExeute(object _)
        {
            _model.SaveSettings();
            App.Current.Shutdown();
        }
    }
}