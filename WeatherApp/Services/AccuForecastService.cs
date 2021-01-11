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
    public class AccuForecastService : IForecastService
    {
        public HttpClient _httpClient;

        public AccuForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<DayForecast>> GetDailyForecasts(string locationKey, ApiSettings apiSettings)
        {
            string url = BuildUri(locationKey, apiSettings, true);
            var response = await _httpClient.GetAsync(url);
            string apiResponseText = await response.Content.ReadAsStringAsync();
            JObject apiResponse = JObject.Parse(apiResponseText);
            IList<JToken> results = apiResponse["DailyForecasts"].Children().ToList();

            IList<DayForecast> forecasts = new List<DayForecast>();
            foreach (JToken result in results)
            {
                DayForecast forecast = new DayForecast();
                forecast.Day = result.Value<DateTime>("Date");

                JToken tempList = result.Value<JToken>("Temperature");
                forecast.MaximumTemp = tempList.Value<JToken>("Maximum").Value<double>("Value");
                forecast.MinimumTemp = tempList.Value<JToken>("Minimum").Value<double>("Value");

                forecasts.Add(forecast);
            }

            return forecasts;
        }

        public async Task<IList<HourForecast>> GetHourlyForecasts(string locationKey, ApiSettings apiSettings)
        {
            string url = BuildUri(locationKey, apiSettings, false);
            var response = await _httpClient.GetAsync(url);
            string apiResponseText = await response.Content.ReadAsStringAsync();
            JArray apiResponse = JArray.Parse(apiResponseText);
            IList<JToken> results = apiResponse.Children().ToList();

            IList<HourForecast> forecasts = new List<HourForecast>();
            foreach (JToken result in results)
            {
                HourForecast forecast = new HourForecast();
                forecast.Time = result.Value<DateTime>("DateTime");

                forecast.Temperature = result.Value<JToken>("Temperature").Value<double>("Value");

                forecasts.Add(forecast);
            }

            return forecasts;
        }

        private string BuildUri(string locationKey, ApiSettings apiSettings, bool isDaily)
        {
            string connectionString = "http://dataservice.accuweather.com/forecasts/v1/" +
                                        (isDaily ? "daily/5day/" : "hourly/12hour/") +
                                        locationKey;

            var builder = new UriBuilder(connectionString);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["apikey"] = apiSettings.Key;
            query["metric"] = "true";
            builder.Query = query.ToString();

            return builder.ToString();
        }
    }
}
