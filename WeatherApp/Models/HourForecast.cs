using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class HourForecast
    {
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:htt}")]
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
    }
}
