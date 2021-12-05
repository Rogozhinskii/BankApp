using BankLibrary.Model.AccountModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    public class AccountInfoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string accountType = string.Empty;            
            string number = string.Empty;
            if (values != null){
                if (values[0].ToString() == AccountType.Deposit.ToString()){
                    accountType = $"Депозитный счет";
                }
                else{
                    accountType = $"Накопительный счет";
                }                
                number = values[1].ToString().Split('-').LastOrDefault().Substring(7,4);
            }
            return $"Номер: ***{number}\n{accountType}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
