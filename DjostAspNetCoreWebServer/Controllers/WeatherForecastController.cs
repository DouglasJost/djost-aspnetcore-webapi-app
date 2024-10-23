using AppServiceCore.Interfaces.WeatherForecast;
using AppServiceCore.Models.WeatherForecast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
    // Defines base URL segment for the controller.
    // Auto replace name of the controller class, excluding the "Controller" suffix.
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherForecastService weatherForecaseService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecaseService;
        }

        [HttpGet]
        public IActionResult GetWeatherForecastUsingService()
         {
            var request = new WeatherForecastRequestDto()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 32,
            };

            var forecast = _weatherForecastService.GetWeatherForecast(request);
            return Ok(forecast);
        }

        [HttpPost]
        public IActionResult GetWeatherForecastUsingService([FromBody] WeatherForecastRequestDto forecastRequest)
        {
            var forecast = _weatherForecastService.GetWeatherForecast(forecastRequest);
            return Ok(forecast);
        }


/*
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
*/
    }
}
