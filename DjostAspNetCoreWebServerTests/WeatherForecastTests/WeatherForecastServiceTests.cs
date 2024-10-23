using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using WeatherLibrary.Services.WeatherForecast;
using AppServiceCore.Models.WeatherForecast;

namespace WeatherForecastServiceTests
{
    [TestFixture]
    public class WeatherForecastServiceTests
    {
        private Mock<ILogger<WeatherForecastService>> _loggerMock;
        private WeatherForecastService _weatherForecastService;

        [SetUp]
        public void Setup() 
        {
            _loggerMock = new Mock<ILogger<WeatherForecastService>>();
            _weatherForecastService = new WeatherForecastService(_loggerMock.Object);
        }

        [Test]
        public void GetWeatherForecast_ShouldReturnSuccess_WithValidRequest()
        {
            // Arrange
            var dateTimeNow = DateTime.Now;
            var request = new WeatherForecastRequestDto
            {
                Date = DateOnly.FromDateTime(dateTimeNow),
                TemperatureC = 25
            };


            // Act
            var result = _weatherForecastService.GetWeatherForecast(request);


            // Assert
            Assert.IsTrue(result.IsSuccess, "The result should be successful.");
            Assert.IsNotNull(result.Value, "The result value should not be null.");
            Assert.AreEqual(request.Date, result.Value.Date, "The date should match the request.");
            Assert.AreEqual(request.TemperatureC, result.Value.TemperatureC, "The temperature in Celsius should match the request.");
            Assert.AreEqual(32 + (int)(request.TemperatureC / 0.5556), result.Value.TemperatureF, "The temperature in Fahrenheit should be correctly calculated.");
            Assert.Contains(
                result.Value.Summary, 
                new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" },
                "The summary should be one of the predefined summaries.");


            // Verify that the logger was called with the expected message
            _loggerMock.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == "Hello from WeatherForecastService"),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once,
                "Expected LogInformation to be called once with the message 'Hello from WeatherForecastService'.");
        }
    }
}
