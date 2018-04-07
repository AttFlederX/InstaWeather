using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace InstaWeather.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _temperatureUnits;
        private string _pressureUnits;
        private string _windSpeedUnits;

        private readonly IPageService _pageService;

        public ICommand SaveCommand { get; private set; }

        public string TemperatureUnits { get => _temperatureUnits; set => SetValue(ref _temperatureUnits, value); }
        public string PressureUnits { get => _pressureUnits; set => SetValue(ref _pressureUnits, value); }
        public string WindSpeedUnits { get => _windSpeedUnits; set => SetValue(ref _windSpeedUnits, value); }

        public App CurrentApp { get; private set; }

        public SettingsViewModel(App currentApp, IPageService pageService)
        {
            _pageService = pageService;

            SaveCommand = new Command(async () => await Save());
            CurrentApp = currentApp;

            // TemperatureUnits property stores the unit name, but returns whtether it's metric or imperial instead of pure value
            TemperatureUnits = (CurrentApp.TemperatureUnits == "imperial") ? "Fahrenheit" : "Celsius";
            PressureUnits = CurrentApp.PressureUnits;
            WindSpeedUnits = CurrentApp.WindSpeedUnits;
        }

        private async Task Save()
        {
            // TemperatureUnits property stores the unit name, but returns whtether it's metric or imperial instead of pure value
            CurrentApp.TemperatureUnits = TemperatureUnits;
            CurrentApp.PressureUnits = PressureUnits;
            CurrentApp.WindSpeedUnits = WindSpeedUnits;

            MessagingCenter.Send(this, Consts.SettingsChanged);
            await _pageService.PopAsync();
        }
    }
}
