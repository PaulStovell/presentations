using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace Example3.WpfSearchQuery
{
    class StringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = string.Empty;

            if (values.Length > 0 && parameter != null)
            {
                result = string.Format(parameter.ToString(), values);
            }

            return result;
        }

        public object[] ConvertBack(object values, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null; // Not used
        }
    }
}
