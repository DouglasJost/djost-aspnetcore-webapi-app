using System;
using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.WeatherForecast
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class WeatherForecastRequestDto
    {
        [Required(ErrorMessage = "Date attribute is required.")]
        public DateOnly Date { get; set; }       // 2024-10-03

        [Required(ErrorMessage = "TemperatureC attribute is required.")]
        public int TemperatureC { get; set; }    // 55
    }
}
