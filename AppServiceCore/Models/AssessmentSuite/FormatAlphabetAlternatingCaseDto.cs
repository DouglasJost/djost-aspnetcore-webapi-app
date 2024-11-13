namespace AppServiceCore.Models.AssessmentSuite
{
    public class FormatAlphabetAlternatingCaseRequestDto
    {
        public bool IsFirstCharUpper {  get; set; } 
    }

    public class FormatAlphabetAlternatingCaseResponseDto : FormatAlphabetAlternatingCaseRequestDto
    {
        public string Alphabet { get; set; } = string.Empty;
    }
}
