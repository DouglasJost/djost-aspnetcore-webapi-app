using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class MinMaxNumbersRequestDto
    {
        [Required(ErrorMessage = "Numbers array attribute is required.")]
        public int[]? Numbers { get; set; }
    }

    public class MinMaxNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public int? MaxNumber { get; set; }
        public int? MinNumber { get; set; }
    }
}
