using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using InstaWeather.Services;
using InstaWeather.ViewModels;

namespace InstaWeather
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        public SettingsViewModel ViewModel
        {
            get => BindingContext as SettingsViewModel;
            set => BindingContext = value;
        }

        public SettingsPage()
		{
            ViewModel = new SettingsViewModel(Application.Current as App, new PageService());
            InitializeComponent();
		}
	}
}