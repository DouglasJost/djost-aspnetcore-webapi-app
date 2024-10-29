using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class RemoveDuplicateNumbersRequestDto
    {
        public int[]? Numbers { get; set; }
    }

    public class RemoveDuplicateNumbersResponseDto
    {
        public int[]? Numbers { get; set; }
        public List<int>? UniqueNumbers { get; set; }
    }
}
