using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class ConvertTimeFormatRequestDto
    {
        [Required(ErrorMessage = "InputTime attribute is required.")]
        public string? InputTime { get; set; }
    }

    public class ConvertTimeFormResponseDto : ConvertTimeFormatRequestDto
    {
        public string OutputTime { get; set; } = string.Empty;
    }
}
