using BankLibrary.Model.AccountModel;
using BankUI.Core.Common;
using System;
using System.Globalization;

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
