using BankLibrary.Model.AccountModel;
using System;

namespace BankLibrary.AccountModel.Interfaces
{
    public interface IAccountManager
    {
        void CreateNewAccount(AccountType type);
        void CloseAccount();

        void SendMoney(Guid fromAccountId, Guid toAccountId, float count);
        

    }
}
