using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class FormatAlphabetAlternatingCaseRequestDto
    {
        public bool IsFirstCharUpper {  get; set; } 
    }

    public class FormatAlphabetAlternatingCaseResponseDto : FormatAlphabetAlternatingCaseRequestDto
    {
        public string Alphabet { get; set; } = string.Empty;
    }
}
