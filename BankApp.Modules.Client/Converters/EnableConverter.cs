using BankApp.Modules.Client.Converters.Base;
using BankLibrary.Model.AccountModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    
    internal class EnableConverter : ValueConverterBase
    {
        /// <summary>
        /// возвращает true если счет депозитный, иначе false
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is AccountType accountType)
            {
                if (accountType == AccountType.Deposit)
                {
                    return true;
                }
            }
            return false;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return base.ConvertBack(value, targetType, parameter, culture);
        }
    }
}
