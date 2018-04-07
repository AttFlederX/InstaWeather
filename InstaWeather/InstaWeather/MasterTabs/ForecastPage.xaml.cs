using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InstaWeather.ViewModels;
using InstaWeather.Services;

namespace InstaWeather
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForecastPage : ContentPage
	{
        public ForecastViewModel ViewModel
        {
            get => BindingContext as ForecastViewModel;
            set => BindingContext = value;
        }

        public ForecastPage()
		{
            ViewModel = new ForecastViewModel(new PageService(), new WeatherService());
            InitializeComponent();

            MessagingCenter.Subscribe<SettingsViewModel>(this, Consts.SettingsChanged, async (source) => await Update()); // for settings update
        }

        protected override async void OnAppearing()
        {
            if (!ViewModel.IsUpdated)
            {
                await Update();
                ViewModel.IsUpdated = true;
            }
            base.OnAppearing();
        }

        private void MasterListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                masterListView.SelectedItem = null;
            }
        }

        private async void MasterListView_Refreshing(object sender, EventArgs e)
        {
            await Update(false);
            masterListView.IsRefreshing = false;
        }


        private async Task Update(bool showLoadingScreen = true)
        {
            App.DeviceCoords = await (new LocationService()).GetCurrentLocation(true);
            await ViewModel.UpdateForecast(showLoadingScreen);
        }
    }
}