using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class DuplicateNumbersRequestDto
    {
        public int[]? Numbers { get; set; }
    }

    public class DuplicateNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public List<int>? Duplicates { get; set; }
    }
}
