using AppServiceCore.Interfaces.TestGorilla;
using AppServiceCore.Models.TestGorilla;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DjostAspNetCoreWebServer.Controllers.TestGorilla
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestGorillaController : ControllerBase
    {
        private readonly ILogger<TestGorillaController> _logger;
        private readonly IIpAddressValidationService _ipAddressValidationService;

        public TestGorillaController(
            ILogger<TestGorillaController> logger,
            IIpAddressValidationService ipAddressValidationService)
        {
            _logger = logger;
            _ipAddressValidationService = ipAddressValidationService;
        }

        [Route("IsValidIpAddress")]
        [HttpPost]
        public IActionResult IsValidIpAddress([FromBody] IpAddressValidationRequestDto request)
        {
            var result = _ipAddressValidationService.IsValidIpAddress(request);
            return Ok(result);
        }
    }
}
