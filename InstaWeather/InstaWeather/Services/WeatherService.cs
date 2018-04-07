using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InstaWeather.ViewModels;

namespace InstaWeather.Services
{
    /// <summary>
    /// Weather service provider that fetches a JSON string from a remote source
    /// </summary>
    public class WeatherService : IWeatherService
    {
        public HttpClient MasterClient { get; private set; }

        public WeatherService()
        {
            MasterClient = new HttpClient();
        }

        /// <summary>
        /// Returns the deserialized result of the remote service query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<dynamic> GetWeatherDataFromService(string query)
        {
            var svcResponse = await MasterClient.GetAsync(query);
            dynamic weatherData = null;

            if (svcResponse != null)
            {
                weatherData = JsonConvert.DeserializeObject(await svcResponse.Content.ReadAsStringAsync()); // returns a Json.Linq.JObject instance
            }
            else { throw new HttpRequestException("Failed to fetch the weather data"); }

            return weatherData;
        }
    }
}
