namespace AppServiceCore.Models.AssessmentSuite
{
    public class ReverseStringRequestDto
    {
        public string Request {  get; set; } = string.Empty;
    }

    public class ReverseStringResponseDto
    {
        public string Request {  get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }
}
