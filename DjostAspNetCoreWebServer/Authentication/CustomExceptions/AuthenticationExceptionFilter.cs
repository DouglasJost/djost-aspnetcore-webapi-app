using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DjostAspNetCoreWebServer.Authentication.CustomExceptions
{
    public class AuthenticationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadRequestException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
                context.ExceptionHandled = true;
            }
            else if (context.Exception is UnauthorizedUserCredentialsException)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
