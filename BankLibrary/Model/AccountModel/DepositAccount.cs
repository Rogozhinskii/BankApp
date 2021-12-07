using Newtonsoft.Json;
using System;

namespace BankLibrary.Model.AccountModel
{
    public class DepositAccount:BankAccount
    {
        /// <summary>
        /// Срок вклада
        /// </summary>
        public int Term { get; set; }

        /// <summary>
        /// Ставка 
        /// </summary>
        public float Rate
        {
            get
            {
                if (ClientType == ClientModel.ClientType.Special)
                {
                    return DepositRates.specialRate;
                }
                return DepositRates.regularRate;
            }
        }

        /// <summary>
        /// Общий эффект по истечению срока ставки
        /// </summary>
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
