using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class ContainsOnlyDigitsRequestDto
    {
        public char[]? Chars {  get; set; }
    }

    public class ContainsOnlyDigitsResponseDto
    {
        public char[]? Chars { set; get; }
        public bool IsAllDigits { get; set; }
    }
}
