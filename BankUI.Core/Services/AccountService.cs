using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services
{
    public class AccountService : IAccountService
    {
        public IAccountManager<IAccount> GetAccountManager(AccountType accountType)
        {
            IAccountManager<IAccount> _accountManager = accountType switch
            {
                AccountType.Deposit => new AccountManager<DepositAccount>(),
                AccountType.Savings => new AccountManager<SavingAccount>(),
                _ => null
            };

            return _accountManager;
        }
    }
}
