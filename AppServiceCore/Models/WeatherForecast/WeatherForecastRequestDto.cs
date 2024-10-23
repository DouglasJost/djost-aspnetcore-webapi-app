using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.WeatherForecast
{
    public class WeatherForecastRequestDto
    {
        public DateOnly Date { get; set; }       // 2024-10-03
        public int TemperatureC { get; set; }    // 55
    }
}
