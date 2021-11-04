using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Model.AccountModel.Interfaces
{
    public interface ITransaction<in T>
    {
        void SendMoneyToAccount(T accaunt, float sum);
        void SendMoneyToClient(T fromAccaunt, T toAccaunt, float sum);
    }
}
