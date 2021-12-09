using BankUI.Core.Common;
using BankUI.Core.Common.Log;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BankApp.Modules.NotificationTools.Converters
{
    public class LogLevelToPicKIndConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = PackIconKind.EventNote;
            if(value !=null && value is LogRecordLevel level)
            {
                return result = level switch
                {
                    LogRecordLevel.Info => PackIconKind.InfoCircle,
                    LogRecordLevel.Error => PackIconKind.Error,
                    LogRecordLevel.Warning => PackIconKind.Warning,
                    LogRecordLevel.None => PackIconKind.QuestionMarkCircle,
                    _=>PackIconKind.Error
                };
            }

            return result;
        }
    }
}
