using System;

namespace DjostAspNetCoreWebServer.Authentication.CustomExceptions
{
    public class UnauthorizedUserCredentialsException : Exception
    {
        public UnauthorizedUserCredentialsException(string message) : base(message) { }
    }
}