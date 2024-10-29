using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class ConvertTimeFormatRequestDto
    {
        public string InputTime { get; set; } = string.Empty;
    }

    public class ConvertTimeFormResponseDto : ConvertTimeFormatRequestDto
    {
        public string OutputTime { get; set; } = string.Empty;
    }
}
