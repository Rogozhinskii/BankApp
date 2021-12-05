using BankLibrary.Model.AccountModel.Interfaces;

namespace BankLibrary.Model.AccountModel
{
    public class TransactionManager<T> : ITransactionManager<T> where T : IAccount
    {
        public void SendMoneyToAccount(T account, float sum)
        {
            account.IncreaseBalance(sum);
        }

        public void SendMoneyToAccount(T fromAccaunt, T toAccaunt, float sum)
        {
            if (fromAccaunt != null && toAccaunt != null)
            {

                if (fromAccaunt.CanReduceBalance(sum))
                {
                    var flag = fromAccaunt.ReduceBalance(sum);
                    toAccaunt.IncreaseBalance(sum);
                }

            }
        }
    }
}
