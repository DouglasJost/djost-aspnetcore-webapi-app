using AppServiceCore;
using AppServiceCore.Interfaces.WeatherForecast;
using AppServiceCore.Logging;
using AppServiceCore.Models.WeatherForecast;
using Microsoft.Extensions.Logging;
using System;

namespace WeatherLibrary.Services.WeatherForecast
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.WeatherLibrary);

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        public WeatherForecastService()
        {
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
