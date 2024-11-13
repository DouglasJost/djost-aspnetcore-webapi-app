using System.Collections.Generic;

namespace AppServiceCore.Models.AssessmentSuite
{
    public class DuplicateNumbersRequestDto
    {
        public int[]? Numbers { get; set; }
    }

    public class DuplicateNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public List<int>? Duplicates { get; set; }
    }
}
