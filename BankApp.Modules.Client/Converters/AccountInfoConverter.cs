using BankLibrary.Model.AccountModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    public class AccountInfoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string accountType = string.Empty;
            string balance = string.Empty;
            if (values != null){
                if (values[0].ToString() == AccountType.Deposit.ToString()){
                    accountType = $"Депозитный счет";
                }
                else{
                    accountType = $"Накопительный счет";
                }
                balance = values[1].ToString();
            }
            return $"{accountType}. Баланс: {balance} $";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
