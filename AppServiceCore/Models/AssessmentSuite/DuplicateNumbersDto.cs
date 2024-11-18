using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class DuplicateNumbersRequestDto
    {
        [Required(ErrorMessage = "Numbers array attribute is required.")]
        public int[]? Numbers { get; set; }
    }

    public class DuplicateNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public List<int>? Duplicates { get; set; }
    }
}
