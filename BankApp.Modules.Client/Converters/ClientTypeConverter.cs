using BankLibrary.Model.ClientModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BankApp.Modules.Client.Converters
{
    public class ClientTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = string.Empty;
            if (value != null)
            {
                if (value.ToString() == ClientType.Special.ToString())
                {
                    type = "Привелегированный клиент";
                }
                else
                {
                    type = "Клиент без привелегий";
                }
            }
            return type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
