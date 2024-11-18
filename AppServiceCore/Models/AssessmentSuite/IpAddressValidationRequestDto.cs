using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class IpAddressValidationRequestDto
    {
        [Required(ErrorMessage = "IpAddress attribute is required.")]
        public string? IpAddress { get; set; }
    }
}
