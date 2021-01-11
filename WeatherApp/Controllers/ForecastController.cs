using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WeatherApp.Data;
using WeatherApp.Models;
using WeatherApp.Models.WeatherViewModels;
using WeatherApp.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApp.Controllers
{
    public class ForecastController : Controller
    {
        ApiSettings _apiSettings;
        ILocationService _locationService;
        IForecastService _forecastService;

        public ForecastController(IOptionsMonitor<ApiSettings> optionsMonitor, ILocationService locationService,
                                    IForecastService forecastService)
        {
            _apiSettings = optionsMonitor.CurrentValue;
            _locationService = locationService;
            _forecastService = forecastService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return RedirectToAction("Index", "Home");
            }

            ForecastsView forecastsView = new ForecastsView();

            Location location = await _locationService.GetLocation(searchString, _apiSettings);
            forecastsView.Location = location;
            forecastsView.DailyForecasts = await _forecastService.GetDailyForecasts(location.LocationKey, _apiSettings);
            forecastsView.HourlyForecasts = await _forecastService.GetHourlyForecasts(location.LocationKey, _apiSettings);

            return View(forecastsView);
        }
    }
}
