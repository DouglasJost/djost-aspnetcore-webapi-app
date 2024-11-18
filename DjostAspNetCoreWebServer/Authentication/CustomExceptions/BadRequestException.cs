using System;

namespace DjostAspNetCoreWebServer.Authentication.CustomExceptions
{
    public class BadRequestException : Exception 
    {
        public BadRequestException(string message) : base(message) { }
    }
}