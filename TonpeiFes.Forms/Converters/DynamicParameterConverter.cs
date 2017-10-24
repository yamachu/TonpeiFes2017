using System;
using System.Globalization;
using Microsoft.CSharp.RuntimeBinder;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class DynamicParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            dynamic _value = value;

            try
            {
                var propertyInfo = _value.GetType().GetProperty(parameter as string);
                return propertyInfo.GetValue(_value, null) as string;
            }
            catch(RuntimeBinderException ex)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
