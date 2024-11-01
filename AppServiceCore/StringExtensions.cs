using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string str) 
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(str.ToLower());
        }
    }
}
