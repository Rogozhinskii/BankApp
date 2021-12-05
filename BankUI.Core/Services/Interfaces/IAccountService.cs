using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;

namespace BankUI.Core.Services.Interfaces
{
    public interface IAccountService
    {
        IAccountManager<IAccount> GetAccountManager(AccountType accountType);


    }
}
