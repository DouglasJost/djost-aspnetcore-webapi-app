namespace AppServiceCore.Interfaces.AssessmentSuite
{
    public interface IIpAddressValidationService
    {
        bool IsValidIpAddress(string ipAddress);
    }
}
