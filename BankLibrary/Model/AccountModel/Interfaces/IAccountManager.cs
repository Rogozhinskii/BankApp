using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;
using System;

namespace BankLibrary.AccountModel.Interfaces
{
    public interface IAccountManager
    {
        IAccount CreateNewAccount(AccountType type);
        void CloseAccount();

        bool SendMoney(Guid fromAccountId, Guid toAccountId, float count);
        

    }
}
