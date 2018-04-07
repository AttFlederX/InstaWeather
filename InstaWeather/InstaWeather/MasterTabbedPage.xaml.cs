using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InstaWeather.Services;

namespace InstaWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterTabbedPage : TabbedPage
    {
        public MasterTabbedPage()
        {
            InitializeComponent();
        }

        //protected override async void OnAppearing()
        //{
        //    //App.DeviceCoords = await (new LocationService()).GetCurrentLocation(true);
        //    //MessagingCenter.Send(this, Consts.LocationReceived);
        //    base.OnAppearing();
        //}

        private async void SettingsToolbarItem_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}