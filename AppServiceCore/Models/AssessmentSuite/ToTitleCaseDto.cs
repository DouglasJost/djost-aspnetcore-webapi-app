namespace AppServiceCore.Models.AssessmentSuite
{
    public class ToTitleCaseRequestDto
    {
        public string Title { get; set; } = string.Empty;
    }

    public class ToTileCaseResponseDto : ToTitleCaseRequestDto
    {
        public string TitleCased { get; set; } = string.Empty;
    }
}
