using System;
using System.Globalization;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class NumberEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(parameter as string, out int target)) return false;
            return (int)value == target;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
