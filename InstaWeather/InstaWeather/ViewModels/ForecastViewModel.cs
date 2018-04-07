using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using InstaWeather.Models;

namespace InstaWeather.ViewModels
{
    public class ForecastViewModel : BaseViewModel
    {
        private ForecastData _forecast;
        private bool _isLoading;

        private IPageService pageService;
        private IWeatherService weatherService;

        public bool IsUpdated { get; set; }

        public ForecastData Forecast { get => _forecast; set => SetValue(ref _forecast, value); }
        public bool IsLoading { get => _isLoading; set => SetValue(ref _isLoading, value); } // displays the loading screen

        public ForecastViewModel(IPageService _pageService, IWeatherService _weatherService)
        {
            pageService = _pageService;
            weatherService = _weatherService;

            IsLoading = true;
            IsUpdated = false;
            Forecast = new ForecastData();
            //{
            //    City = "San Francisco",
            //    Country = "United States",
            //    DailyForecast = new List<ForecastDailyData>()
            //    {
            //        new ForecastDailyData("4/5/2018")
            //        {
            //            new ForecastHourlyData()
            //            {
            //                Description = "Clear",
            //                Temperature = 101,
            //                Pressure = 1018,
            //                Humidity = 93,
            //                WindSpeed = 2,
            //                WindHeading = 161,
            //                CloudCoverage = 1,
            //                UpdateTime = DateTime.Now,
            //                Icon = "01d"
            //            }
            //        }
            //    }
            //};
        }

        public async Task UpdateForecast(bool showLoadingScreen = true)
        {
            IsLoading = showLoadingScreen;
            (double lat, double lng) location = App.DeviceCoords;
            string query = $"http://api.openweathermap.org/data/2.5/forecast?lat={location.lat}&lon={location.lng}&appid={Consts.Key}&" +
                $"units={(Application.Current as App).TemperatureUnits}";

            try
            {
                var results = await weatherService.GetWeatherDataFromService(query);
                // pares the returned dictionary into ForecastData
                Forecast = ForecastData.Parse(results);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
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
