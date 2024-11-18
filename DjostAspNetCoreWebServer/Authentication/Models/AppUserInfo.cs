namespace DjostAspNetCoreWebServer.Authentication.Models
{
    public class AppUserInfo
    {
        public string UserId { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        public AppUserInfo() { }

        public AppUserInfo(
            string userId,
            string login,
            string firstName,
            string lastName,
            string city,
            string state)
        {
            UserId = userId;
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            State = state;
        }
    }
}
