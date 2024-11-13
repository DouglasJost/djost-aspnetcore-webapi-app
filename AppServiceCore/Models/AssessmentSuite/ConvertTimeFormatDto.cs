namespace AppServiceCore.Models.AssessmentSuite
{
    public class ConvertTimeFormatRequestDto
    {
        public string InputTime { get; set; } = string.Empty;
    }

    public class ConvertTimeFormResponseDto : ConvertTimeFormatRequestDto
    {
        public string OutputTime { get; set; } = string.Empty;
    }
}
