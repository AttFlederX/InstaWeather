using System;
using System.Collections.Generic;
using System.Text;

namespace InstaWeather.Models
{
    /// <summary>
    /// Class for grouping the hourly forecasts into days
    /// </summary>
    public class ForecastDailyData : List<ForecastHourlyData>
    {
        public string Date { get; private set; }

        public ForecastDailyData(string date)
        {
            Date = date;
        }
    }
}
