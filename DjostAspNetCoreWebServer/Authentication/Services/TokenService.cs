using DjostAspNetCoreWebServer.Authentication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DjostAspNetCoreWebServer.Authentication.CustomExceptions;

namespace DjostAspNetCoreWebServer.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IValidateUserRepository _validateUserRepository;

        public TokenService(
            IConfiguration configuration,
            IValidateUserRepository validateUserRepository)
        {
            _configuration = configuration;
            _validateUserRepository = validateUserRepository;
        }

        public string CreateJwtSecurityToken(string userName, string password)
        {
            // Retrieve authentication attributes from appsettings
            var base64Secret = _configuration["Authentication:SecretForKey"];
            var issuer = _configuration["Authentication:Issuer"];
            var audience = _configuration["Authentication:Audience"];

            if (string.IsNullOrEmpty(base64Secret) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new BadRequestException("Authentication configuration values are missing");
            }

            // Validate user credentials
            var appUserInfo = _validateUserRepository.ValidateUserCredentials(userName, password);
            if (appUserInfo == null ||
                string.IsNullOrEmpty(appUserInfo.UserId) ||
                string.IsNullOrEmpty(appUserInfo.Login) ||
                string.IsNullOrEmpty(appUserInfo.LastName) ||
                string.IsNullOrEmpty(appUserInfo.FirstName) ||
                string.IsNullOrEmpty(appUserInfo.State) ||
                string.IsNullOrEmpty(appUserInfo.City))
            {
                throw new UnauthorizedUserCredentialsException("Application user information values are missing");
            }

            // Generate symmetric security key
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(base64Secret));

            // Generate signing credentials
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create claims for the token
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", appUserInfo.Login),
                new Claim("given_name", appUserInfo.FirstName),
                new Claim("family_name", appUserInfo.LastName),
                new Claim("city", appUserInfo.City),
                new Claim("state", appUserInfo.State),
            };

            // Create the JWT security token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer,
                audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            // Write out the token to a string that can be returned to the caller
            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }

        public string GenerateSecurityKey(string key)
        {
            // Convert the string to a byte array
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Convert the byte array to a base64 string
            return Convert.ToBase64String(keyBytes);
        }
    }
}
