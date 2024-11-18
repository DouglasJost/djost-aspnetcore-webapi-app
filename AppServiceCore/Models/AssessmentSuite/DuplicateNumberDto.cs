using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class DuplicateNumberRequestDto
    {
        [Required(ErrorMessage = "Numbers array attribute is required.")]
        public int[]? Numbers {  get; set; }
    }

    public class DuplicateNumberResponseDto
    {
        public int[]? Numbers { get; set; }
        public int? DuplicateNumber { get; set; }
    }
}
