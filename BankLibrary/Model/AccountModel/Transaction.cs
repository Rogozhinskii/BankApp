using BankLibrary.Model.AccountModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.AccountModel
{
    public class Transaction<T> : ITransaction<T> where T : IAccount
    {
        public void SendMoneyToAccount(T account, float sum)
        {
            account.IncreaseBalance(sum);
        }

        public void SendMoneyToClient(T fromAccaunt, T toAccaunt, float sum)
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
