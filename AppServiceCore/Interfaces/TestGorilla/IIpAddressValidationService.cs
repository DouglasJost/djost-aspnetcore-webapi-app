using AppServiceCore.Models.TestGorilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Interfaces.TestGorilla
{
    public interface IIpAddressValidationService
    {
        CommandResult<IpAddressValidationResponseDto> IsValidIpAddress(IpAddressValidationRequestDto request);
    }
}
