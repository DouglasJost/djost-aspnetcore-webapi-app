using DjostAspNetCoreWebServer.Authentication.Models;

namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface IValidateUserRepository
    {
        AppUserInfo ValidateUserCredentials(string userName, string password);
    }
}
