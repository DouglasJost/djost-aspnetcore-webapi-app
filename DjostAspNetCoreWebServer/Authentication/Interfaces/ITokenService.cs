namespace DjostAspNetCoreWebServer.Authentication.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtSecurityToken(string userName, string password);
        string GenerateSecurityKey(string key);
    }
}
