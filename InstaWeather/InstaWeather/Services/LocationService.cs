using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using InstaWeather.ViewModels;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace InstaWeather.Services
{
    public class LocationService : ILocationService
    {
        /// <summary>
        /// Returns the current (or cached) coordinates of the device
        /// </summary>
        /// <param name="cacheOverride">Set to true if location has to be updated rather than fetched from the cache</param>
        /// <returns>The latitude and longitude of the device's coordinates</returns>
        public async Task<(double lat, double lng)> GetCurrentLocation(bool cacheOverride = false)
        {
            Position position = null;
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;

            if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
            {
                //not available or enabled
                throw new GeolocationException(GeolocationError.Unauthorized);
            }

            if (!cacheOverride)
            {
                position = await locator.GetLastKnownLocationAsync();
                if (position != null) { return (position.Latitude, position.Longitude); }
            }
            position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            if (position != null) { return (position.Latitude, position.Longitude); }
            else { throw new GeolocationException(GeolocationError.PositionUnavailable); }
        }
    }
}
