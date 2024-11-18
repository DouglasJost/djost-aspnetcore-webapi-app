using System.ComponentModel.DataAnnotations;

namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class RemoveSpecialCharactersRequestDto
    {
        [Required(ErrorMessage = "Sentence attribute is required.")]
        [MaxLength(50, ErrorMessage = "Sentence attribute must be between 1 and 50 characters in lenbth.")]
        public string? Sentence { get; set; }
    }

    public class RemoveSpecialCharactersResponseDto
    {
        public string OriginalSentence { get; set; } = string.Empty;
        public string SentenceWithoutSpecialCharacters { get; set; } = string.Empty;
        public string RemovedSpecialCharacters {  get; set; } = string.Empty;
    }

}
