using BankLibrary.AccountModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.AccountModel
{
    class NonDepositAccount:BankAccount
    {
        public NonDepositAccount(AccountType type = AccountType.NonDeposit) : base(type) { }
        
    }
}
