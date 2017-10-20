using System;
using System.Globalization;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class IsNullOrEmptyOrWhitespaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is string)) return true;
            return string.IsNullOrEmpty(value as string) || string.IsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
