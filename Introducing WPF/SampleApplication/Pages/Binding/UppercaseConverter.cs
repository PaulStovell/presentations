using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace SampleApplication.Pages.Binding
{
    class UppercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value ?? string.Empty).ToString().ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value ?? string.Empty).ToString().ToUpper();
        }
    }
}
