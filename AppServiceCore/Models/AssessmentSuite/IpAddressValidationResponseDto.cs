using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.AssessmentSuite
{
    public class IpAddressValidationResponseDto
    {
        public string IpAddress { get; set; }
        public bool IsValidAddress { get; set; }
    }
}
