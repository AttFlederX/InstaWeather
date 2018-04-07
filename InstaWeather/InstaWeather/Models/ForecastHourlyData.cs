using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace InstaWeather.Models
{
    /// <summary>
    /// Class for storing an hourly forecast entry
    /// </summary>
    public class ForecastHourlyData
    {
        public string Description { get; set; }
        public int Temperature { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public int WindHeading { get; set; }
        public int CloudCoverage { get; set; }
        public DateTime UpdateTime { get; set; }

        public string Icon { get; set; }
        public ImageSource IconPath { get => ImageSource.FromResource($"InstaWeather.Images.{Icon}.png"); }

        public string TemperatureString { get => $"{Temperature}° { (((Application.Current as App).TemperatureUnits == "imperial") ? "F" : "C") }"; }
        public string PressureString
        {
            get
            {
                if ((Application.Current as App).PressureUnits == "inHg")
                { return $"{(Pressure / 33.86).ToString("N2")} inHg"; }
                else { return $"{Pressure} hPa"; }
            }
        }
        public string WindString
        {
            get // wind speed returned by the API depends on temperature units
            {
                var app = Application.Current as App;
                if (app.TemperatureUnits == "imperial")
                {
                    if (app.WindSpeedUnits == "km/h") { return $"{WindHeading}° @ {(WindSpeed * 0.621).ToString("N0")} km/h"; }
                    if (app.WindSpeedUnits == "kts") { return $"{WindHeading}° @ {(WindSpeed * 1.15).ToString("N0")} kts"; }
                    else { return $"{WindHeading}° @ {WindSpeed} mph"; }
                }
                else
                {
                    if (app.WindSpeedUnits == "mph") { return $"{WindHeading}° @ {(WindSpeed * 1.609).ToString("N0")} mph"; }
                    if (app.WindSpeedUnits == "kts") { return $"{WindHeading}° @ {(WindSpeed * 1.852).ToString("N0")} kts"; }
                    else { return $"{WindHeading}° @ {WindSpeed} km/h"; }
                }
            }
        }

        // Format as "<DayOfWeek>, <Three-letter month> <Day>"
        public string UpdateDateString { get => $"{UpdateTime.DayOfWeek.ToString()}, {UpdateTime.ToString("MMM")} {UpdateTime.Day}"; }
        // Format as "<Hour> AM/PM"
        public string UpdateTimeString { get => $"{UpdateTime.ToString("h tt")}"; }
    }
}
