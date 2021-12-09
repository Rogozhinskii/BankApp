using BankLibrary.Model.AccountModel.Interfaces;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// Отвечает за переводы средств между счетами клиентов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TransactionManager<T> : ITransactionManager<T> where T : IAccount
    {
        public bool SendMoneyToAccount(T account, float sum)
        {
            return account.IncreaseBalance(sum);
        }

        public bool SendMoneyToAccount(T fromAccaunt, T toAccaunt, float sum)
        {
            bool result = false;
            if (fromAccaunt != null && toAccaunt != null){
                result = fromAccaunt.ReduceBalance(sum);
                if (result)
                {
                    toAccaunt.IncreaseBalance(sum);
                }
            }
            return result;
        }
    }
}
