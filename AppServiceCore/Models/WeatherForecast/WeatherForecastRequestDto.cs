using System;

namespace AppServiceCore.Models.WeatherForecast
{
    public class WeatherForecastRequestDto
    {
        public DateOnly Date { get; set; }       // 2024-10-03
        public int TemperatureC { get; set; }    // 55
    }
}
