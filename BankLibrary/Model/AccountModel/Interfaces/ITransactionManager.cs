namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface ITransactionManager<in T>
    {
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
