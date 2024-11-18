using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class CompareNumberToValueRequestDto
    {
        [Required(ErrorMessage = "CompareValue int attribute is required.")]
        public int? CompareValue { get; set; }

        [Required(ErrorMessage = "Numbers array attribute is required.")]
        public int[]? Numbers { get; set; }
    }

    public class CompareNumberToValueResponseDto : CompareNumberToValueRequestDto
    {
        public int LessThanCount { get; set; }
        public int EqualToCount { get; set; }
        public int GreaterThanCount { get; set; }
    }
}
