using AppServiceCore;
using AppServiceCore.Interfaces.TestGorilla;
using AppServiceCore.Models.TestGorilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGorillaLibrary.Services
{
    public class IpAddressValidationService : IIpAddressValidationService
    {
        public CommandResult<IpAddressValidationResponseDto> IsValidIpAddress(IpAddressValidationRequestDto request)
        {
            var ipAddress = request.IpAddress;

            var response = new IpAddressValidationResponseDto()
            {
                IpAddress = ipAddress,
                IsValidAddress = true,
            };

            // Usage examples
            //Console.WriteLine(IsValidIp("192.168.1.1"));    // True
            //Console.WriteLine(IsValidIp("192.168.01.1"));  // False
            //Console.WriteLine(IsValidIp("256.100.50.25")); // False
            //Console.WriteLine(IsValidIp("192.168.1"));     // False
            //Console.WriteLine(IsValidIp("192.168.1.001")); // False
            //Console.WriteLine(IsValidIp("10.0.0.0"));       // True

            string[] parts = ipAddress.Split('.');
            if (parts.Length != 4) // An IP address should exactly have 4 octets
            {
                response.IsValidAddress = false;
            }

            if (response.IsValidAddress)
            {
                foreach (var part in parts)
                {
                    if (!int.TryParse(part, out var parsedPart))
                    {
                        // Fail if part is not an integer
                        response.IsValidAddress = false;
                        break;
                    }
                    if (parsedPart < 0 || parsedPart > 255)
                    {
                        // Fail if part is not in the range of 0 to 255
                        response.IsValidAddress = false;
                        break;
                    }
                    // Check for leading zeros:
                    if (part != "0" && part.StartsWith("0") || part.Length > 3)
                    {
                        // Fail if part has leading zeros (unless it is exactly "0")
                        response.IsValidAddress = false;
                        break;
                    }
                }
            }

            return CommandResult<IpAddressValidationResponseDto>.Success(response);
        }
    }
}
