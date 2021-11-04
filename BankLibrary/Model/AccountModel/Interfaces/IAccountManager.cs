using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using System;

namespace BankLibrary.AccountModel.Interfaces
{
    public interface IAccountManager<out T>
    {
        
        T CreateNewAccount(float sum);
        
        void CloseAccount();

        bool SendMoney(Guid fromAccountId, Guid toAccountId, float count);
        
        
    }
}
