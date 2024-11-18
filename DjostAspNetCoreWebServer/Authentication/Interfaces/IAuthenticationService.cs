using AppServiceCore;
using DjostAspNetCoreWebServer.Authentication.Models;

namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        CommandResult<SecurityTokenResponseDto> CreateSecurityToken(SecurityTokenRequestDto request);
        CommandResult<GenerateSecretResponseDto> GenerateSecurityKey(GenerateSecretRequestDto request);
    }
}
