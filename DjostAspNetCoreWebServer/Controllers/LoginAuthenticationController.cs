using Asp.Versioning;
using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Controllers
{
    // http://localhost:5149/api/v1/LoginAuthentication/Authenticate

    [ApiController]
    [Route("api/v{version:apiVersion}/LoginAuthentication")]
    [ApiVersion(1)]
    public class LoginAuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginAuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("GenerateSecret")]
        [HttpPost]
        public IActionResult GenerateSecret([FromBody] GenerateSecretRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = _authenticationService.GenerateSecurityKey(request);
            return Ok(response);
        }

        [Route("Authenticate")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] SecurityTokenRequestDto request)
        {
            // User application singleton Serilog logger.
            //var _logger = AppServiceCore.Loggers.AppSerilogLogger.GetLogger(AppServiceCore.Loggers.SerilogLoggerCategoryType.LoginAuthentication);
            //_logger.Information("This is a test INFORMATION message");
            //_logger.Debug("This is a test DEBUG message");
            // var _logger = Log.ForContext("SourceContext", "LoginAuthentication");

            // Use general or default Serilog logger.
            //var _nonCategoryLogger = Log.Logger;
            //_nonCategoryLogger.Information("This is a test INFORMATION message");
            //_nonCategoryLogger.Debug("This is a test DEBUG message");

            var response = await _authenticationService.CreateSecurityTokenAsync(request);
            return Ok(response);
        }

        [Route("HashPassword")]
        [HttpPost]
        public IActionResult HashPassword([FromBody] string password) 
        {
            var response = _authenticationService.HashPassword(password);
            return Ok(response);    
        }
    }
}
