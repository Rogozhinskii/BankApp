using BankLibrary.Model.ClientModel;
using BankUI.Core.Common;
using System;
using System.Globalization;


namespace BankApp.Modules.Client.Converters
{
    internal class ClientTypeConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = string.Empty;
            if (value != null){
                if (value.ToString() == ClientType.Special.ToString()){
                    type = "Привелегированный клиент";
                }
                else{
                    type = "Клиент без привелегий";
                }
            }
            return type;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
