namespace BankLibrary.AccountModel.Interfaces
{
    /// <summary>
    /// Ковариантный интерфейс создания счетов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAccountManager<out T>
    {
        /// <summary>
        /// Создает и возвращает счет типа Т, и начисляет на него количество средств равное sum
        /// </summary>      
        /// <param name="sum"></param>
        /// <returns></returns>
        T CreateNewAccount(float sum);

        /// <summary>
        /// Создает и возвращает счет типа Т
        /// </summary>
        /// <returns></returns>
        T CreateNewAccount();
        
    }
}
