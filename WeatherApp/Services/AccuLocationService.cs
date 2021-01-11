using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class AccuLocationService : ILocationService
    {
        public HttpClient _httpClient;

        public AccuLocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Location> GetLocation(string searchString, ApiSettings apiSettings)
        {
            IList<Location> location = await GetLocations(searchString, apiSettings);
            return location[0];
        }

        public async Task<IList<Location>> GetLocations(string searchString, ApiSettings apiSettings)
        {
            string url = BuildUri(searchString, apiSettings);
            var response = await _httpClient.GetAsync(url);
            string apiResponseText = await response.Content.ReadAsStringAsync();
            JArray apiResponse = JArray.Parse(apiResponseText);
            IList<JToken> results = apiResponse.Children().ToList();

            IList<Location> locations = new List<Location>();
            foreach (JToken result in results)
            {
                Location location = new Location();
                location.LocationKey = result.Value<string>("Key");
                location.LocationName = result.Value<string>("EnglishName");
                locations.Add(location);
            }

            return locations;
        }

        private string BuildUri(string searchString, ApiSettings apiSettings)
        {
            var builder = new UriBuilder("http://dataservice.accuweather.com/locations/v1/search");
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["apikey"] = apiSettings.Key;
            query["q"] = searchString;
            builder.Query = query.ToString();

            return builder.ToString();
        }
    }
}
