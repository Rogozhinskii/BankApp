﻿using BankApp.Modules.Client.Converters.Base;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Globalization;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    /// <summary>
    /// Включает textbщх суммы перевода со сберегательного счета
    /// </summary>
    internal class SelectedToEnableConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is SavingAccount)
            {
                return true;
            }
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return base.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
