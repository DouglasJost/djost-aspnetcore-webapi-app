using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class ReverseStringRequestDto
    {
        [Required(ErrorMessage = "Request attribute is required.")]
        [MaxLength(50, ErrorMessage = "Request attribute must be between 1 and 50 characters in lenbth.")]
        public string? Request {  get; set; }
    }

    public class ReverseStringResponseDto
    {
        public string Request {  get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }
}
