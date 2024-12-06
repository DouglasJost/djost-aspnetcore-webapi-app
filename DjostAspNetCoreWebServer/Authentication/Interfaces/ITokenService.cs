using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtSecurityTokenAsync(string? login, string? password);
        string GenerateSecurityKey(string key);
    }
}
