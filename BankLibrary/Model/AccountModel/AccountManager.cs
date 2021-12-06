using BankLibrary.AccountModel;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel
{
    
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
            T acc = new T();
            acc.Id = Guid.NewGuid();                      
            return acc;
        }

    }
}
