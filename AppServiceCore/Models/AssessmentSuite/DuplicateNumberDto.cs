using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.AssessmentSuite
{
    public class DuplicateNumberRequestDto
    {
        public int[]? Numbers {  get; set; }
    }

    public class DuplicateNumberResponseDto
    {
        public int[]? Numbers { get; set; }
        public int? DuplicateNumber { get; set; }
    }
}
