using AppServiceCore.Interfaces.WeatherForecast;
using AppServiceCore.Models.WeatherForecast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DjostAspNetCoreWebServer.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    // Defines base URL segment for the controller.
    // Auto replace name of the controller class, excluding the "Controller" suffix.
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(
            IWeatherForecastService weatherForecaseService)
        {
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
    }
}
