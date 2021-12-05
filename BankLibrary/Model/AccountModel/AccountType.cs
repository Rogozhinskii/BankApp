using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace BankLibrary.Model.AccountModel
{

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
