﻿using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    class SelectedToEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is IAccount)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value==null && value is IAccount)
            {
                return false;
            }
            return true;
        }
    }
}
