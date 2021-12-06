using Newtonsoft.Json;
using System;

namespace BankLibrary.Model.AccountModel
{
    public class DepositAccount:BankAccount
    {
        public int Term { get; set; }
        public float Rate
        {
            get
            {
                if (this.ClientType == ClientModel.ClientType.Special)
                {
                    return DepositRates.specialRate;
                }
                return DepositRates.regularRate;
            }
        }

        public float TotalIncome
        {
            get
            {
                return Balance * Convert.ToSingle(Math.Pow(1 + Rate / 12, Term));
            }
        }
        public DepositAccount() : base(AccountType.Deposit) { }

        [JsonConstructor]
        public DepositAccount(Guid id, float balance) : base(id, balance, AccountType.Deposit) { }
    }
}
