using BankLibrary.AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankLibrary.Model.AccountModel
{
    class NonDepositAccount:BankAccount
    {
        
        [JsonConstructor]
        public NonDepositAccount(Guid id, float balance, Client owner, AccountType accountType) : base(id, balance, owner, accountType)
        {
        }
    }
}
