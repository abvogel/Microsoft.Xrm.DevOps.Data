using System.Globalization;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// The goal is to make numbers parseable with . or , as decimal separator. Note these chars for some culture could also be numb group separators.
        /// This should be safe to do as when exporting from Configuration Migration Tool the numbers doesn't include any number group separators
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string NormalizeSeparator(this string input)
        {
            if (string.IsNullOrEmpty(input) || CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Length == 0)
            {
                return input;
            }
            var invalidSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? "," : ".";
            return input.Replace(invalidSeparator, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }
    }
}
