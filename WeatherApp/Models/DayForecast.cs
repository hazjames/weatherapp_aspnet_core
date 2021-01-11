using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class DayForecast
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd d MMMM}")]
        public DateTime Day { get; set; }
        public double MinimumTemp { get; set; }
        public double MaximumTemp { get; set; }
    }
}
