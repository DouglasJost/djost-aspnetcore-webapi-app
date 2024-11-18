using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class RemoveDuplicateNumbersRequestDto
    {
        [Required(ErrorMessage = "Numbers array attribute is required.")]
        public int[]? Numbers { get; set; }
    }

    public class RemoveDuplicateNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public List<int>? UniqueNumbers { get; set; }
    }
}
