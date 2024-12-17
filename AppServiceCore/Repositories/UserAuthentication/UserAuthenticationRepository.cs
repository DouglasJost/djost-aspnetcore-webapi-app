using AppDomainEntities;
using AppDomainEntities.Entities;
using AppServiceCore.AutoMapper;
using AppServiceCore.Interfaces.Authentication;
using AppServiceCore.Models.Authentication;
using AppServiceCore.Util;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Repositories.UserAuthentication
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly IAutoTypeMapper<UserLogin, AuthenticationDto> _authenticationMapper;

        public UserAuthenticationRepository(IAutoTypeMapper<UserLogin, AuthenticationDto> authenticationMapper)
        {
            _authenticationMapper = authenticationMapper ?? throw new ArgumentNullException(nameof(authenticationMapper));
        }

        public async Task<AuthenticationDto?> AuthenticationUserAsync(MusicCollectionDbContext dbContext, string? login, string? password)
        {
            AuthenticationDto? authenticationDto = null;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return authenticationDto;
            }

            UserLogin? userLoginEntity = await dbContext.UserLogins
                .AsNoTracking()
                .Include(ul => ul.UserAccount)
                .Where(ul => ul.Login == login)
                .FirstOrDefaultAsync();

            if (userLoginEntity == null)
            {
                return null;
            }

            var isValid = PasswordHasher.VerifyPassword(password, userLoginEntity.Password);
            if (!isValid)
            {
                return null;
            }

            authenticationDto = _authenticationMapper.Map(userLoginEntity);
            return authenticationDto;
        }
    }
}
