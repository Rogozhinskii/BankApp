using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;

namespace BankUI.Core.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для реализаии возможности создания пользовательских счетов в UI
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Возвращает параметризированный,экземпляр класса, реализиющий интерфейс IAccountManager - отвечает за создание новых счетов соответсвующего типа
        /// Если AccountType=AccountType.Deposit, вернет объект, способный создать DepositAccount.
        /// По умолчанию вернет IAccountManager<SavingAccount>
        /// </summary>
        /// <param name="accountType">тип счета, которым нужно параметризировать IAccount</param>
        /// <returns></returns>
        IAccountManager<IAccount> GetAccountManager(AccountType accountType);
    }
}
