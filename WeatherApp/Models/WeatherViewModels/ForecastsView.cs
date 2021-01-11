using System;
using System.Collections.Generic;

namespace WeatherApp.Models.WeatherViewModels
{
    public class ForecastsView
    {
        public Location Location { get; set; }
        public IList<DayForecast> DailyForecasts { get; set; }
        public IList<HourForecast> HourlyForecasts { get; set; }
    }
}
