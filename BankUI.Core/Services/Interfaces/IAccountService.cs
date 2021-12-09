using BankLibrary.AccountModel.Interfaces;
using BankLibrary.Model.AccountModel;
using BankLibrary.Model.AccountModel.Interfaces;

namespace BankUI.Core.Services.Interfaces
{
    /// <summary>
    /// Интерфейс для реализаии возможности создания пользовательских счетов в UI
    /// </summary>
    public interface IAccountService<T>
    {
        /// <summary>
        /// Создает и возвращает счет типа Т, и начисляет на него количество средств равное sum
        /// </summary>      
        /// <param name="sum"></param>
        /// <returns></returns>
        T CreateNewAccount(AccountType accountType);

        /// <summary>
        /// Осущесвтляет перевод со счет T, равный сумме sum.
        /// </summary>
        /// <param name="accaunt"></param>
        /// <param name="sum"></param>
        bool SendMoneyToAccount(T accaunt, float sum);

        /// <summary>
        /// Осуществляет перевод со счета fromAccaunt на счет toAccaunt равный сумме sum. Если перевод произведен возвращает true, иначе false
        /// </summary>
        /// <param name="fromAccaunt"></param>
        /// <param name="toAccaunt"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        bool SendMoneyToAccount(T fromAccaunt, T toAccaunt, float sum);
    }
}
