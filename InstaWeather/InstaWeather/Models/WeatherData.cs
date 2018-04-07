using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace InstaWeather.Models
{
    /// <summary>
    /// Class for storing current weather data
    /// </summary>
    public class WeatherData
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Temperature { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public int WindHeading { get; set; }
        public int CloudCoverage { get; set; }
        public DateTime SunriseTime { get; set; }
        public DateTime SunsetTime { get; set; }
        public DateTime UpdateTime { get; set; }
        
        public string Icon { get; set; }
        public ImageSource IconPath { get => ImageSource.FromResource($"InstaWeather.Images.{Icon}.png"); }

        public string LocationString { get => $"{City}, {Country}"; }
        public string TemperatureString { get => $"{Temperature}° { (((Application.Current as App).TemperatureUnits == "imperial") ? "F" : "C") }"; }
        public string PressureString {
            get
            {
                if ((Application.Current as App).PressureUnits == "inHg")
                { return $"{(Pressure / 33.86).ToString("N2")} inHg"; } // leave 2 decimal spaces after point
                else { return $"{Pressure} hPa"; }
            }
        }
        public string WindString {
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
        public string SunriseTimeString { get => SunriseTime.ToShortTimeString(); }
        public string SunsetTimeString { get => SunsetTime.ToShortTimeString(); }
        public string UpdateTimeString { get => UpdateTime.ToString(); }


        public static WeatherData Parse(dynamic queryResponse)
        {
            return new WeatherData()
            {
                City = (string)queryResponse["name"],
                // converts the country code into a full name
                Country = (new System.Globalization.RegionInfo((string)queryResponse["sys"]["country"]).DisplayName),
                Description = (string)queryResponse["weather"][0]["description"],
                Temperature = (int)queryResponse["main"]["temp"],
                Pressure = (int)queryResponse["main"]["pressure"],
                Humidity = (int)queryResponse["main"]["humidity"],
                WindSpeed = (int)queryResponse["wind"]["speed"],
                WindHeading = (int)queryResponse["wind"]["deg"],
                CloudCoverage = (int)queryResponse["clouds"]["all"],
                UpdateTime = DateTimeOffset.FromUnixTimeSeconds((int)queryResponse["dt"]).ToLocalTime().ToLocalTime().DateTime,
                SunriseTime = DateTimeOffset.FromUnixTimeSeconds((int)queryResponse["sys"]["sunrise"]).ToLocalTime().DateTime,
                SunsetTime = DateTimeOffset.FromUnixTimeSeconds((int)queryResponse["sys"]["sunset"]).ToLocalTime().DateTime,

                Icon = (string)queryResponse["weather"][0]["icon"]
            };
        }
    }
}
