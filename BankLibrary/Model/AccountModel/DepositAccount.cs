using BankLibrary.AccountModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankLibrary.Model.AccountModel
{
    public class DepositAccount:BankAccount
    {
        public DepositAccount() : base(AccountType.Deposit) { }

        [JsonConstructor]
        public DepositAccount(Guid id, float balance) : base(id, balance, AccountType.Deposit) { }
    }
}
