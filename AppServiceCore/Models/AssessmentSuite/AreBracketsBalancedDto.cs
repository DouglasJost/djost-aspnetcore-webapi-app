using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.AssessmentSuite
{
  public class AreBracketsBalancedRequestDto
  {
    public string TestString { get; set; } = string.Empty;
  }

  public class AreBracketsBalancedResponseDto
  {
    public string TestString { get; set; } = string.Empty;
    public bool AreBalanced { get; set; } = false;
  }
}
