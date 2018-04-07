using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaWeather.ViewModels
{
    public interface IWeatherService
    {
        Task<dynamic> GetWeatherDataFromService(string query);
    }
}
