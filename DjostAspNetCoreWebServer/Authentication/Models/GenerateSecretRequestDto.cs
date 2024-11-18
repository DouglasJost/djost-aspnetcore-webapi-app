using System.ComponentModel.DataAnnotations;

namespace DjostAspNetCoreWebServer.Authentication.Models
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //
    public class GenerateSecretRequestDto
    {
        [Required(ErrorMessage = "An attribute is missing or invalid.")]
        [MaxLength(200, ErrorMessage = "An attribute is missing or invalid.")]
        public string? KeySecret { get; set; }
    }
}
