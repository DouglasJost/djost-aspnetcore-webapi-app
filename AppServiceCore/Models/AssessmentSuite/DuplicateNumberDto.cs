namespace AppServiceCore.Models.AssessmentSuite
{
    public class DuplicateNumberRequestDto
    {
        public int[]? Numbers {  get; set; }
    }

    public class DuplicateNumberResponseDto
    {
        public int[]? Numbers { get; set; }
        public int? DuplicateNumber { get; set; }
    }
}
