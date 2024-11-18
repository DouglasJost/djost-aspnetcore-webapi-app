namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class FormatAlphabetAlternatingCaseRequestDto
    {
        public bool IsFirstCharUpper {  get; set; } 
    }

    public class FormatAlphabetAlternatingCaseResponseDto : FormatAlphabetAlternatingCaseRequestDto
    {
        public string Alphabet { get; set; } = string.Empty;
    }
}
