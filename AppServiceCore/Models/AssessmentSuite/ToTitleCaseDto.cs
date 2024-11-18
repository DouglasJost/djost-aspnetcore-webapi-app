using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class ToTitleCaseRequestDto
    {
        [Required(ErrorMessage = "Title attribute is required.")]
        [MaxLength(50, ErrorMessage = "Title attribute must be between 1 and 50 characters in lenbth.")]
        public string? Title { get; set; }
    }

    public class ToTileCaseResponseDto : ToTitleCaseRequestDto
    {
        public string TitleCased { get; set; } = string.Empty;
    }
}
