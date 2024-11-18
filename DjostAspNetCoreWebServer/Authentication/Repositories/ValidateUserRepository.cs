using DjostAspNetCoreWebServer.Authentication.Interfaces;
using DjostAspNetCoreWebServer.Authentication.Models;

namespace DjostAspNetCoreWebServer.Authentication.Repositories
{
    public class ValidateUserRepository : IValidateUserRepository
    {
        public AppUserInfo ValidateUserCredentials(string userName, string password)
        {
            var appUserInfo = ValidateUser(userName, password);
            return appUserInfo;
        }

        private AppUserInfo ValidateUser(string? userName, string? password)
        {
            return new AppUserInfo()
            {
                UserId = "Fred",
                Login = userName ?? string.Empty,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                City = "Dallas",
                State = "Texas",
            };
        }
    }
}
