using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaWeather.ViewModels
{
    /// <summary>
    /// Interface for lat-long positionprovider classes
    /// </summary>
    public interface ILocationService
    {
        Task<(double lat, double lng)> GetCurrentLocation(bool cacheOverride = false);
    }
}
