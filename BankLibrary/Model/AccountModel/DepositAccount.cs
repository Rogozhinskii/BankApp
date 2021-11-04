using BankLibrary.AccountModel;
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
        public DepositAccount():base()
        {

        }

        [JsonConstructor]
        public DepositAccount(Guid id, float balance,AccountType type) : base(id, balance,type)
        {
        }
    }
}
