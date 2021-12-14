using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.Exceptions;
using Newtonsoft.Json;
using System;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// Накопительный счет
    /// </summary>
    public class SavingAccount:BankAccount
    {
        public SavingAccount() : base(AccountType.Savings) { }

        [JsonConstructor]
        public SavingAccount(Guid id, float balance) : base(id, balance, AccountType.Savings) { }


        /// <summary>
        /// Перегрузка оператора + , как бы имитация процесса слияния счетов. Будет работать только с накопительными счетами, т.к. вряд ли можно сложить депозиты))
        /// </summary>
        /// <param name="firstAccount"></param>
        /// <param name="secondAccount"></param>
        /// <returns></returns>
        public static IAccount operator + (SavingAccount firstAccount, SavingAccount secondAccount)
        {
            if (firstAccount.Id == secondAccount.Id)
                throw new SameAccountsException();
            return new SavingAccount(Guid.NewGuid(), firstAccount.Balance + secondAccount.Balance);            
        }

       
    }


    
}
