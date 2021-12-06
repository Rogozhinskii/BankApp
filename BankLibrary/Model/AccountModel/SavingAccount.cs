using BankLibrary.Model.ClientModel.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Model.AccountModel
{
    public class SavingAccount:BankAccount
    {
        public SavingAccount() : base(AccountType.Savings)
        {

        }

        [JsonConstructor]
        public SavingAccount(Guid id, float balance) : base(id, balance, AccountType.Savings)
        {
        }
    }
}
