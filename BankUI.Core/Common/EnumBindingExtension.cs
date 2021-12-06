using System;
using System.Windows.Markup;

namespace BankUI.Core.Common
{
    /// <summary>
    /// расширение разметки для привязки перечислений
    /// </summary>
    public class EnumBindingExtension : MarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return _enumType; }
            set
            {
                if (value != _enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException($"Нужно перечисление:{nameof(enumType)}");
                        _enumType = value;
                    }
                }
            }
        }
        public EnumBindingExtension() { }

        public EnumBindingExtension(Type enumType)
        {
            EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == _enumType)
                throw new ArgumentNullException(nameof(_enumType));
            Type currentEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
            Array enumValues = Enum.GetValues(currentEnumType);
            if (currentEnumType == _enumType)
                return enumValues;
            Array tempArray = Array.CreateInstance(currentEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }
}
