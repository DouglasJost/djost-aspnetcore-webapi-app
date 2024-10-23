using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGorillaLibrary.Services;
using AppServiceCore.Models.TestGorilla;
using AppServiceCore.Interfaces.TestGorilla;
using WeatherLibrary.Services.WeatherForecast;

namespace DjostAspNetCoreWebServerTests.TestGorillaTests
{
    [TestFixture]
    public class IpAddressValidationServiceTests
    {
        // IpAddressValidationService
        private IpAddressValidationService _ipAddressValidationService;

        [SetUp]
        public void Setup() 
        {
            _ipAddressValidationService = new IpAddressValidationService();
        }

        [Test]
        [TestCase("192.168.1.1")]
        [TestCase("10.0.0.0")]
        public void IsValidIpAddress_ShouldReturnSuccess_WithValidRequest(string ipAddress)
        {
            var request = new IpAddressValidationRequestDto { IpAddress = ipAddress };
            var response = _ipAddressValidationService.IsValidIpAddress(request);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.Value.IsValidAddress, Is.True);
            Assert.That(response.Value.IpAddress, Is.EqualTo(ipAddress));
        }

        [Test]
        [TestCase("192.168.01.1")]
        [TestCase("256.100.50.25")]
        [TestCase("192.168.1")]
        [TestCase("192.168.1.001")]
        public void IsValidIpAddress_ShouldReturnFailure_WithInvalidRequest(string ipAddress)
        {
            var request = new IpAddressValidationRequestDto { IpAddress = ipAddress };
            var response = _ipAddressValidationService.IsValidIpAddress(request);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.Value.IsValidAddress, Is.False);
            Assert.That(response.Value.IpAddress, Is.EqualTo(ipAddress));
        }
    }
}
