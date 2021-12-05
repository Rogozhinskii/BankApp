using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using System;

namespace BankLibrary.AccountModel.Interfaces
{
    public interface IAccountManager<out T>
    {
        
        T CreateNewAccount(float sum);
        T CreateNewAccount();

        void CloseAccount();

        //todo удоооолить  эту херню
        bool SendMoney(Guid fromAccountId, Guid toAccountId, float count);
        
        
    }
}
