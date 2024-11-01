using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.TestGorilla
{
    public class ToTitleCaseRequestDto
    {
        public string Title { get; set; } = string.Empty;
    }

    public class ToTileCaseResponseDto : ToTitleCaseRequestDto
    {
        public string TitleCased { get; set; } = string.Empty;
    }
}
