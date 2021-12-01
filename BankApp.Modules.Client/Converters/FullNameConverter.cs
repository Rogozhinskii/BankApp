using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    public class FullNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string name = string.Empty;
            string surname = string.Empty;
            if (values != null)
            {
                name = values.FirstOrDefault().ToString();
                surname = values.LastOrDefault().ToString();
            }
            return $"{name} {surname}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
