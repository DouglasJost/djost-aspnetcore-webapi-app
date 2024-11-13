using Microsoft.Extensions.Logging;
using System;
using WeatherLibrary.Services.WeatherForecast;
using AppServiceCore.Models.WeatherForecast;
using AppServiceCore.Logging;
using DjostAspNetCoreWebServerTests;

namespace WeatherForecastServiceTests
{
    [TestFixture]
    public class WeatherForecastServiceTests
    {
        private MockLoggerProvider _mockLoggerProvider;

        [SetUp]
        public void Setup()
        {
            _mockLoggerProvider = new MockLoggerProvider();
            var loggerFactory = new LoggerFactory(new[] { _mockLoggerProvider });
            AppLogger.InitializeLogger(loggerFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _mockLoggerProvider.Dispose();
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
            var weatherForecastService = new WeatherForecastService();
            var result = weatherForecastService.GetWeatherForecast(request);

            // Assert
            Assert.IsTrue(result.IsSuccess, "The result should be successful.");
            Assert.IsNotNull(result.Value, "The result value should not be null.");
            Assert.That(result.Value.Date, Is.EqualTo(request.Date), "The date should match the request.");
            Assert.That(result.Value.TemperatureC, Is.EqualTo(request.TemperatureC), "The temperature in Celsius should match the request.");
            Assert.That(result.Value.TemperatureF, Is.EqualTo(32 + (int)(request.TemperatureC / 0.5556)), "The temperature in Fahrenheit should be correctly calculated.");
            Assert.Contains(
                result.Value.Summary, 
                new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" },
                "The summary should be one of the predefined summaries.");


            // Verify that the logger was called with the expected message
            //_loggerMock.Verify(
            //    logger => logger.Log(
            //        LogLevel.Information,
            //        It.IsAny<EventId>(),
            //        It.Is<It.IsAnyType>((v, t) => v.ToString() == "Hello from WeatherForecastService"),
            //        It.IsAny<Exception>(),
            //        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
            //    Times.Once,
            //    "Expected LogInformation to be called once with the message 'Hello from WeatherForecastService'.");
        }
    }
}
