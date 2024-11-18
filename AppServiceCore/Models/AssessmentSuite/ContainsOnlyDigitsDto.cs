using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class ContainsOnlyDigitsRequestDto
    {
        [Required(ErrorMessage = "Chars array attribute is required.")]
        public char[]? Chars {  get; set; }
    }

    public class ContainsOnlyDigitsResponseDto
    {
        public char[]? Chars { set; get; }
        public bool IsAllDigits { get; set; }
    }
}
