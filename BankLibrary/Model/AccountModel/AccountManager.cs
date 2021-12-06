using BankLibrary.AccountModel;
using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using BankLibrary.Model.DataRepository;
using BankLibrary.Model.DataRepository.Interfaces;
using System;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AccountManager<T> : IAccountManager<T> where T:IAccount, new()
    {
        public AccountManager() { }
       
        public T CreateNewAccount(ClientType type,float sum)
        {
            T acc = CreateNewAccount(type);
            acc.IncreaseBalance(sum);
            return acc;
        }

        public T CreateNewAccount(ClientType type)
        {
            T acc = new T();
            acc.Id = Guid.NewGuid();
            acc.ClientType = type;            
            return acc;
        }

    }
}
