using AppDomainEntities;
using System.Threading.Tasks;

namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJwtSecurityTokenAsync(MusicCollectionDbContext dbContext, string? login, string? password);
        string GenerateSecurityKey(string key);
    }
}
