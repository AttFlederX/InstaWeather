using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace InstaWeather
{
	public partial class App : Application
	{
        public static (double, double) DeviceCoords { get; set; }

        // Resource dictionary entries
        public string TemperatureUnits
        {
            get
            {
                if (Properties.ContainsKey(Consts.TemperatureUnitsKey))
                {
                    return (Properties[Consts.TemperatureUnitsKey].ToString() == "Fahrenheit") ? "imperial" : "metric";
                }
                else { return "metric"; }
            }
            set
            {
                Properties[Consts.TemperatureUnitsKey] = value;
            }
        }

        public string PressureUnits
        {
            get
            {
                if (Properties.ContainsKey(Consts.PressureUnitsKey))
                {
                    return Properties[Consts.PressureUnitsKey].ToString();
                }
                else { return "hPa"; }
            }
            set
            {
                Properties[Consts.PressureUnitsKey] = value;
            }
        }

        public string WindSpeedUnits
        {
            get
            {
                if (Properties.ContainsKey(Consts.WindSpeedUnitsKey))
                {
                    return Properties[Consts.WindSpeedUnitsKey].ToString();
                }
                else { return "km/h"; }
            }
            set
            {
                Properties[Consts.WindSpeedUnitsKey] = value;
            }
        }


        public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new InstaWeather.MasterTabbedPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
