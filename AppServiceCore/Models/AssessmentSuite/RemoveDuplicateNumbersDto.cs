using System.Collections.Generic;

namespace AppServiceCore.Models.AssessmentSuite
{
    public class RemoveDuplicateNumbersRequestDto
    {
        public int[]? Numbers { get; set; }
    }

    public class RemoveDuplicateNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public List<int>? UniqueNumbers { get; set; }
    }
}
