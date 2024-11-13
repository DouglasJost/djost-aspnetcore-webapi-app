using System;

namespace AppServiceCore.Models.WeatherForecast
{
    public class WeatherForecastResponseDto
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string? Summary { get; set; }
    }
}
