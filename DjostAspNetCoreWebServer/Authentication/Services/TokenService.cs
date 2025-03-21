﻿using DjostAspNetCoreWebServer.Authentication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DjostAspNetCoreWebServer.Authentication.CustomExceptions;
using System.Threading.Tasks;
using AppServiceCore.Interfaces.Authentication;
using AppDomainEntities;
using AppServiceCore.Services.KeyVaultService;

namespace DjostAspNetCoreWebServer.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly KeyVaultService _keyVaultService;
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;

        public TokenService(
            KeyVaultService keyVaultService,
            IUserAuthenticationRepository userAuthenticationRepository)
        {
            _keyVaultService = keyVaultService;
            _userAuthenticationRepository = userAuthenticationRepository;
        }

        public async Task<string> CreateJwtSecurityTokenAsync(MusicCollectionDbContext dbContext, string? login, string? password)
        {
            // Retrieve authentication attributes from appsettings
            var base64Secret = await _keyVaultService.GetSecretValueAsync(KeyVaultSecretNames.Authentication_SecretForKey);
            var issuer = await _keyVaultService.GetSecretValueAsync(KeyVaultSecretNames.Authentication_Issuer);
            var audience = await _keyVaultService.GetSecretValueAsync(KeyVaultSecretNames.Authentication_Audience);

            if (string.IsNullOrWhiteSpace(base64Secret) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new BadRequestException("Authentication configuration values are missing");
            }

            // Validate user credentials
            var userAuthenticationDto = await _userAuthenticationRepository.AuthenticationUserAsync(dbContext, login, password);
            if (userAuthenticationDto == null || 
                string.IsNullOrWhiteSpace(userAuthenticationDto.Login) ||
                string.IsNullOrWhiteSpace(userAuthenticationDto.UserFirstName) ||
                string.IsNullOrWhiteSpace(userAuthenticationDto.UserLastName))
            {
                throw new UnauthorizedUserCredentialsException("Application user information values are missing.");
            }
            if (userAuthenticationDto.UserLoginInactive || userAuthenticationDto.UserAccountInactive)
            {
                throw new UnauthorizedUserCredentialsException("User login is inactive.");
            }

            // Generate symmetric security key
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(base64Secret));

            // Generate signing credentials
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create claims for the token
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", userAuthenticationDto.Login),
                new Claim("given_name", userAuthenticationDto.UserFirstName),
                new Claim("family_name", userAuthenticationDto.UserLastName),
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
