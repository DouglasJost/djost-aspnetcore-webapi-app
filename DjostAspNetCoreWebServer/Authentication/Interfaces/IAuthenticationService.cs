using AppServiceCore;
using DjostAspNetCoreWebServer.Authentication.Models;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<CommandResult<SecurityTokenResponseDto>> CreateSecurityTokenAsync(SecurityTokenRequestDto request);
        CommandResult<GenerateSecretResponseDto> GenerateSecurityKey(GenerateSecretRequestDto request);
    }
}
