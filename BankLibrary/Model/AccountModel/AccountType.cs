using System.ComponentModel;

namespace BankLibrary.Model.AccountModel
{

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum AccountType
    {
        [Description("Депозит")]
        Deposit,
        [Description("Накопительный")]
        Savings
    }
}
