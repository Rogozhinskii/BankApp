using BankApp.Modules.Client.Converters.Base;
using BankLibrary.Model.AccountModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    /// <summary>
    /// Преобразует значение типа счета в видимость объекта
    /// </summary>
    internal class AccoutTypeToVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
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

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return base.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
