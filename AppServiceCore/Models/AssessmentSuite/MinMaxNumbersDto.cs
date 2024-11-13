namespace AppServiceCore.Models.AssessmentSuite
{
    public class MinMaxNumbersRequestDto
    {
        public int[]? Numbers { get; set; }
    }

    public class MinMaxNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public int? MaxNumber { get; set; }
        public int? MinNumber { get; set; }
    }
}
