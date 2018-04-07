using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InstaWeather.Models
{
    /// <summary>
    /// Class for storing forecast data
    /// </summary>
    public class ForecastData
    {
        public string City { get; set; }
        public string Country { get; set; }

        public string LocationString { get => $"{City}, {Country}"; }

        public List<ForecastDailyData> DailyForecast { get; set; }

        public ForecastData()
        {
            DailyForecast = new List<ForecastDailyData>(5); // forecast is for 5 days
        }

        public static ForecastData Parse(dynamic queryResponse)
        {
            var result = new ForecastData()
            {
                City = (string)queryResponse["city"]["name"],
                // converts the country code into a full name
                Country = (new System.Globalization.RegionInfo((string)queryResponse["city"]["country"]).DisplayName)
            };

            for (int i = 0; i < queryResponse["list"].Count; i++) // "list" is an array of hourly forecasts
            {
                var hourForecast = new ForecastHourlyData()
                {
                    UpdateTime = DateTimeOffset.FromUnixTimeSeconds((int)queryResponse["list"][i]["dt"]).ToLocalTime().DateTime,

                    Description = (string)queryResponse["list"][i]["weather"][0]["description"],
                    Temperature = (int)queryResponse["list"][i]["main"]["temp"],
                    Pressure = (int)queryResponse["list"][i]["main"]["pressure"],
                    Humidity = (int)queryResponse["list"][i]["main"]["humidity"],
                    WindSpeed = (int)queryResponse["list"][i]["wind"]["speed"],
                    WindHeading = (int)queryResponse["list"][i]["wind"]["deg"],
                    CloudCoverage = (int)queryResponse["list"][i]["clouds"]["all"],

                    Icon = (string)queryResponse["list"][i]["weather"][0]["icon"]
                };

                // attempt to find the appropriate daily forecast group
                var dailyForecast = result.DailyForecast.FirstOrDefault(df => (df.Date == hourForecast.UpdateDateString));

                if (dailyForecast == null) // if the group doesn't exist
                {
                    dailyForecast = new ForecastDailyData(hourForecast.UpdateDateString);
                    result.DailyForecast.Add(dailyForecast);
                }

                dailyForecast.Add(hourForecast);
            }

            return result;
        }
    }
}
