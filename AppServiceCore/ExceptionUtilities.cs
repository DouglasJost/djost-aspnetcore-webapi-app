using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore
{
    public class ExceptionUtilities
    {
        public static string AppendExceptionMessages(Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append(ex.Message);

            var inner = ex.InnerException;
            while (inner != null)
            {
                sb.Append(Environment.NewLine);
                sb.Append("Inner Exception: ");
                sb.Append(inner.Message);
                inner = inner.InnerException;
            }

            return sb.ToString();
        }
    }
}
