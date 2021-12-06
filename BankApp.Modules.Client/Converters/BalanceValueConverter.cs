using System;
using System.Globalization;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    class BalanceValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {           
            if (value != null)
            {
                float balance = 0f;
                if (float.TryParse(value.ToString(), out balance))
                {
                    return $"{balance}";
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float result = 0f;
            if (value != null)
            {
                var tt = value.ToString().Replace('$','0').Replace(',','.');
                float.TryParse(tt, out result);
                return result;
            }
            return result;
        }
    }
}
