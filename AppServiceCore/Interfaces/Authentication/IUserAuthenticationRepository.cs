using AppDomainEntities;
using AppServiceCore.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.Authentication
{
    public interface IUserAuthenticationRepository
    {
        Task<AuthenticationDto?> AuthenticationUserAsync(MusicCollectionDbContext dbContext, string? login, string? password);
    }
}