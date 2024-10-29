using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class ReverseStringRequestDto
    {
        public string Request {  get; set; } = string.Empty;
    }

    public class ReverseStringResponseDto
    {
        public string Request {  get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }
}
