using System;
using System.Text;

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
