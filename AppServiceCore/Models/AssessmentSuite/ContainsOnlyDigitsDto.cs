namespace AppServiceCore.Models.AssessmentSuite
{
    public class ContainsOnlyDigitsRequestDto
    {
        public char[]? Chars {  get; set; }
    }

    public class ContainsOnlyDigitsResponseDto
    {
        public char[]? Chars { set; get; }
        public bool IsAllDigits { get; set; }
    }
}
