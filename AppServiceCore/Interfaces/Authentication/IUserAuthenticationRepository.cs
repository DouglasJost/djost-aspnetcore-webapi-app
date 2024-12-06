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
        Task<AuthenticationDto?> AuthenticationUserAsync(string? userName, string? password);
    }
}
