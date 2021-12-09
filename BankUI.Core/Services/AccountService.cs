using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankUI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankUI.Core.Services
{
    public class AccountService<T> : IAccountService<T> where T:IAccount
    {
        private IAccountManager<IAccount> _accountManager;
        ITransactionManager<IAccount> _transactionManager;

        public AccountService()
        {
            _transactionManager = new TransactionManager<IAccount>();
        }
        public T CreateNewAccount(AccountType accountType)
        {
            _accountManager = GetAccountManager(accountType);
            return (T)_accountManager.CreateNewAccount();
        }

        public bool SendMoneyToAccount(T accaunt, float sum)
        {
            return _transactionManager.SendMoneyToAccount(accaunt, sum);
        }

        public bool SendMoneyToAccount(T fromAccaunt, T toAccaunt, float sum)
        {
            return _transactionManager.SendMoneyToAccount(fromAccaunt, toAccaunt, sum);
        }


        /// <summary>
        /// Возвращает параметризированный,экземпляр класса, реализиющий интерфейс IAccountManager - отвечает за создание новых счетов соответсвующего типа
        /// Если AccountType=AccountType.Deposit, вернет объект, способный создать DepositAccount.
        /// По умолчанию вернет IAccountManager<SavingAccount>
        /// </summary>
        /// <param name="accountType">тип счета, которым нужно параметризировать IAccount</param>
        /// <returns></returns>
        private IAccountManager<IAccount> GetAccountManager(AccountType accountType)
        {
            IAccountManager<IAccount> _accountManager = accountType switch
            {
                AccountType.Deposit => new AccountManager<DepositAccount>(),
                AccountType.Savings => new AccountManager<SavingAccount>(),
                _ => new AccountManager<SavingAccount>()
            };

            return _accountManager;
        }
    }
}
