using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// Отвечает за создание новый счетов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AccountManager<T> : IAccountManager<T> where T:IAccount, new()
    {
        public AccountManager() { }
       
        public T CreateNewAccount(float sum)
        {
            T acc = CreateNewAccount();
            acc.IncreaseBalance(sum);
            return acc;
        }

        public T CreateNewAccount()
        {
            T acc = new()
            {
                Id = Guid.NewGuid()
            };
            return acc;
        }

    }
}
