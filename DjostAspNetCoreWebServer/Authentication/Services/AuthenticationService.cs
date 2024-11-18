using AppServiceCore;
using AppServiceCore.Logging;
using DjostAspNetCoreWebServer.Authentication.CustomExceptions;
using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.AppLogger);
        private readonly ITokenService _tokenService;

        public AuthenticationService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public CommandResult<SecurityTokenResponseDto> CreateSecurityToken(SecurityTokenRequestDto request)
        {
            try
            {
                var jwtSecurityToken = _tokenService.CreateJwtSecurityToken(request.UserName, request.Password);
                var response = new SecurityTokenResponseDto
                {
                    JwtSecurityToken = jwtSecurityToken,
                };

                return CommandResult<SecurityTokenResponseDto>.Success(response);
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error creating JWT security token.");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");

                _logger.LogError(sbError.ToString());

                if (ex is BadRequestException) throw;
                if (ex is UnauthorizedUserCredentialsException) throw;

                return CommandResult<SecurityTokenResponseDto>.Failure(sbError.ToString());
            }
        }

        public CommandResult<GenerateSecretResponseDto> GenerateSecurityKey(GenerateSecretRequestDto request)
        {
            var securityKey = _tokenService.GenerateSecurityKey(request.KeySecret);
            var response = new GenerateSecretResponseDto
            {
                Base64Secret = securityKey,
            };

            return CommandResult<GenerateSecretResponseDto>.Success(response);
        }
    }
}
