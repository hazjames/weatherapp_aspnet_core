using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Data;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IForecastService
    {
        Task<IList<DayForecast>> GetDailyForecasts(string locationKey, ApiSettings apiSettings);

        Task<IList<HourForecast>> GetHourlyForecasts(string locationKey, ApiSettings apiSettings);
    }
}
