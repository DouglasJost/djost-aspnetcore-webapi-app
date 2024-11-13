using System.Globalization;

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
