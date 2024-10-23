using AppServiceCore;
using AppServiceCore.Interfaces.WeatherForecast;
using AppServiceCore.Models.WeatherForecast;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary.Services.WeatherForecast
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger<WeatherForecastService> _logger;

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        public WeatherForecastService(ILogger<WeatherForecastService> logger)
        {
            _logger = logger;
        }

        public CommandResult<WeatherForecastResponseDto> GetWeatherForecast(WeatherForecastRequestDto request)
        {
            _logger.LogInformation("Hello from WeatherForecastService");

            var forecast = new WeatherForecastResponseDto()
            {
                Date = request.Date,
                TemperatureC = request.TemperatureC,
                TemperatureF = (32 + (int)(request.TemperatureC / 0.5556)),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            };

            return CommandResult<WeatherForecastResponseDto>.Success(forecast);
        }
    }
}
