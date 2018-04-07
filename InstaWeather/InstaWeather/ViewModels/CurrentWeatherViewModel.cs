using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.CSharp;

using Xamarin.Forms;
using InstaWeather.Models;

namespace InstaWeather.ViewModels
{
    public class CurrentWeatherViewModel : BaseViewModel
    {
        private List<WeatherData> _weatherCell;
        private WeatherData _weather;
        private bool _isLoading;

        private IPageService pageService;
        private IWeatherService weatherService;

        public bool IsUpdated { get; set; }

        public List<WeatherData> WeatherCell { get => _weatherCell; set => SetValue(ref _weatherCell, value); } // for ListView
        public WeatherData Weather { get => _weather; set => SetValue(ref _weather, value); }
        public bool IsLoading { get => _isLoading; set => SetValue(ref _isLoading, value); } // displays the loading screen

        public CurrentWeatherViewModel(IPageService _pageService, IWeatherService _weatherService)
        {
            pageService = _pageService;
            weatherService = _weatherService;

            IsLoading = true;
            IsUpdated = false;
            // weather will be updated every time a new location is received
            // Xamarin.Forms.MessagingCenter.Subscribe<object>(this, Events.LocationReceived, async (source) => await UpdateWeather());

            Weather = new WeatherData();
            //{
            //    City = "San Francisco",
            //    Country = (new System.Globalization.RegionInfo("US").DisplayName),
            //    Description = "Clear",
            //    Temperature = 101,
            //    Pressure = 1018,
            //    Humidity = 93,
            //    WindSpeed = 2,
            //    WindHeading = 161,
            //    CloudCoverage = 1,
            //    UpdateTime = DateTime.Now,
            //    Icon = "01d"
            //};
            WeatherCell = new List<WeatherData>() { Weather };
        }

        public async Task UpdateWeather(bool showLoadingScreen = true)
        {
            IsLoading = showLoadingScreen;
            (double lat, double lng) location = App.DeviceCoords;
            string query = $"http://api.openweathermap.org/data/2.5/weather?lat={location.lat}&lon={location.lng}&appid={Consts.Key}&" + 
                $"units={(Application.Current as App).TemperatureUnits}";

            try
            {
                var results = await weatherService.GetWeatherDataFromService(query);

                // parse the result into a WeatherData object
                Weather = WeatherData.Parse(results);
                WeatherCell = new List<WeatherData>() { Weather }; // update the ListView
            }
            catch (Exception ex)
            {
                await pageService.DisplayAlert("Error", ex.Message, "Close");
                pageService.Close();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
