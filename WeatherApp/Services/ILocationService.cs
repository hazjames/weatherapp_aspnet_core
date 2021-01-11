using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface ILocationService
    {
        Task<Location> GetLocation(string searchString, ApiSettings apiSettings);

        Task<IList<Location>> GetLocations(string searchString, ApiSettings apiSettings);
    }
}
