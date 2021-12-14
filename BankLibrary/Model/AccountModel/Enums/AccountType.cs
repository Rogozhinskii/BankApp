using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// Перечисление возможных типов счета
    /// </summary>
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AccountType
    {
        [Description("Депозит")]
        [EnumMember(Value = "Deposit")]
        Deposit,
        [Description("Накопительный")]
        [EnumMember(Value = "Savings")]
        Savings
    }
}
