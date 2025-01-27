using AppDomainEntities;
using AppServiceCore;
using AppServiceCore.Logging;
using AppServiceCore.Util;
using DjostAspNetCoreWebServer.Authentication.CustomExceptions;
using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDbContextFactory<MusicCollectionDbContext> _dbContextFactory;
        private readonly ILogger _logger = AppLogger.GetLogger(LoggerCategoryType.AppLogger);
        private readonly ITokenService _tokenService;

        public AuthenticationService(
            IDbContextFactory<MusicCollectionDbContext> dbContextFactory,
            ITokenService tokenService)
        {
            _dbContextFactory = dbContextFactory;
            _tokenService = tokenService;
        }

        public async Task<CommandResult<SecurityTokenResponseDto>> CreateSecurityTokenAsync(SecurityTokenRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    return CommandResult<SecurityTokenResponseDto>.Failure("Request cannot be null.");
                }

                //using (var dbContext = new MusicCollectionDbContext(MusicCollectionDbContext.GetDbContextOptions()))
                await using (var dbContext = await _dbContextFactory.CreateDbContextAsync())
                {
                    string jwtSecurityToken = await _tokenService.CreateJwtSecurityTokenAsync(dbContext, request.Login, request.Password);
                    var response = new SecurityTokenResponseDto
                    {
                        JwtSecurityToken = jwtSecurityToken,
                    };
                    return CommandResult<SecurityTokenResponseDto>.Success(response);
                }
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

        public CommandResult<string> HashPassword(string password)
        {
            try 
            {
                var response = PasswordHasher.HashPassword(password);
                return CommandResult<string>.Success(response);
            }
            catch (Exception ex)
            {
                var sbError = new StringBuilder("Error hashing password.");
                sbError.AppendLine($"{Environment.NewLine}  {ex.GetType().ToString()}");
                sbError.AppendLine($"{Environment.NewLine}  {ExceptionUtilities.AppendExceptionMessages(ex)}");

                _logger.LogError(sbError.ToString());

                //return CommandResult<string>.Failure(sbError.ToString());
                throw;
            }
        }
    }
}
