using System;
namespace TonpeiFes.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhitespace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
    }
}
