using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    class SelectedToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is IAccount acc)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value==null && value is IAccount acc)
            {
                return false;
            }
            return true;
        }
    }
}
