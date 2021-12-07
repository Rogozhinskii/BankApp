using BankApp.Modules.Client.Converters.Base;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    internal class FullNameConverter : MultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
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

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return base.ConvertBack(value, targetTypes, parameter, culture);
        }
    }
}
