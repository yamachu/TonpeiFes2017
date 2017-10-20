using System;
namespace TonpeiFes.MobileCore.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhitespace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}
