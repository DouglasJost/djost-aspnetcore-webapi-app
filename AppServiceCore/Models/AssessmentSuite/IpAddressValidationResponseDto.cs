namespace AppServiceCore.Models.AssessmentSuite
{
    //
    // TODO : Use FluentValidation
    // https://docs.fluentvalidation.net/en/latest/index.html
    //

    public class IpAddressValidationResponseDto
    {
        public string? IpAddress { get; set; }
        public bool IsValidAddress { get; set; }
    }
}
