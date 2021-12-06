using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using BankLibrary.Model.ClientModel;
using System;

namespace BankLibrary.AccountModel.Interfaces
{
    public interface IAccountManager<out T>
    {
        
        T CreateNewAccount(ClientType type,float sum);
        T CreateNewAccount(ClientType type);
        
    }
}
