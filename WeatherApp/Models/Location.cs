using System;
using System.Web;
using Microsoft.Extensions.Options;
using WeatherApp.Data;

namespace WeatherApp.Models
{
    public class Location
    {
        public string LocationName { get; set; }
        public string LocationKey { get; set; }
    }
}
