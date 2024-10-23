using AppServiceCore.Models.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.WeatherForecast
{
    public interface IWeatherForecastService
    {
        CommandResult<WeatherForecastResponseDto> GetWeatherForecast(WeatherForecastRequestDto request);
    }
}
