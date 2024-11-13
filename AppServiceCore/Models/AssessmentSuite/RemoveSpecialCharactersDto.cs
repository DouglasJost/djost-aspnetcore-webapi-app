namespace AppServiceCore.Models.AssessmentSuite
{
    public  class RemoveSpecialCharactersRequestDto
    {
        public string Sentence { get; set; } = string.Empty;
    }

    public class RemoveSpecialCharactersResponseDto
    {
        public string OriginalSentence { get; set; } = string.Empty;
        public string SentenceWithoutSpecialCharacters { get; set; } = string.Empty;
        public string RemovedSpecialCharacters {  get; set; } = string.Empty;
    }

}
