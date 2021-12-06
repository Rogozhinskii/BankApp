using BankLibrary.Model.AccountModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    public class AccoutTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is AccountType accountType)
            {
                if (accountType == AccountType.Deposit)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
