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

namespace AppServiceCore.Repositories
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly MusicCollectionDbContext _context;
        private readonly IAutoTypeMapper<UserLogin, AuthenticationDto> _authenticationMapper;

        public UserAuthenticationRepository(
            MusicCollectionDbContext context,
            IAutoTypeMapper<UserLogin, AuthenticationDto> authenticationMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _authenticationMapper = authenticationMapper ?? throw new ArgumentNullException(nameof(authenticationMapper));
        }

        public async Task<AuthenticationDto?> AuthenticationUserAsync(string? login, string? password)
        {
            AuthenticationDto? authenticationDto = null;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                return authenticationDto;
            }

            UserLogin? userLoginEntity = await _context.UserLogins
                .AsNoTracking()
                .Include(ul => ul.UserAccount)
                //.Where(ul => ul.Login == login && ul.Password == password)
                .Where(ul => ul.Login == login)
                .FirstOrDefaultAsync();

            if (userLoginEntity == null)
            {
                return null;
            }

            // TODO: Change StringCipher so I do not compare unencrypted passwords.
            //       Should compare encrypted passwords and if equal, then user is authenticated.
            //       At least unencrypted passwords are not stored in the database. 
            var decryptedPassword = StringCipher.Decrypt(userLoginEntity.Password);
            if (decryptedPassword != password)
            {
                return null;
            }

            authenticationDto = _authenticationMapper.Map(userLoginEntity);

            return authenticationDto;
        }
    }
}
