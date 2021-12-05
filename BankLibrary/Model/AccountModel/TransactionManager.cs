using BankLibrary.Model.AccountModel.Interfaces;

namespace BankLibrary.Model.AccountModel
{
    public class TransactionManager<T> : ITransactionManager<T> where T : IAccount
    {
        public void SendMoneyToAccount(T account, float sum)
        {
            account.IncreaseBalance(sum);
        }

        public bool SendMoneyToAccount(T fromAccaunt, T toAccaunt, float sum)
        {
            bool result = false;
            if (fromAccaunt != null && toAccaunt != null)
            {

                if (fromAccaunt.CanReduceBalance(sum))
                {
                    result = fromAccaunt.ReduceBalance(sum);
                    toAccaunt.IncreaseBalance(sum);
                }

            }
            return result;
        }
    }
}
