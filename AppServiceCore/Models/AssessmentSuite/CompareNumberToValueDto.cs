namespace AppServiceCore.Models.AssessmentSuite
{
    public class CompareNumberToValueRequestDto
    {
        public int? CompareValue { get; set; }
        public int[]? Numbers { get; set; }
    }

    public class CompareNumberToValueResponseDto : CompareNumberToValueRequestDto
    {
        public int LessThanCount { get; set; }
        public int EqualToCount { get; set; }
        public int GreaterThanCount { get; set; }
    }
}
