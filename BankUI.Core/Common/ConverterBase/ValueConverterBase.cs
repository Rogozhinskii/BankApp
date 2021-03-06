using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace BankUI.Core.Common
{
    /// <summary>
    /// Базовый класс для конвертации привязок
    /// </summary>
    public abstract class ValueConverterBase : IValueConverter
    {
        /// <summary>
        /// Выполняет прямое преобразование входных парамметров
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Выполняет обратное преобразование входных парамметров
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
