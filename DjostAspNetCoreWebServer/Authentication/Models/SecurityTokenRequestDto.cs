using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.Authentication.Models
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //
    public class SecurityTokenRequestDto
    {
        [Required(ErrorMessage = "An attribute is missing or invalid.")]
        [MaxLength(50, ErrorMessage = "An attribute is missing or invalid.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "An attribute is missing or invalid.")]
        [MaxLength(50, ErrorMessage = "An attribute is missing or invalid.")]
        public string? Password { get; set; }
    }
}
