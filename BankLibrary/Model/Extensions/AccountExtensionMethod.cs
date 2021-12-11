using BankLibrary.Model.AccountModel.Interfaces;

namespace BankLibrary.Model.AccountModel
{
    /// <summary>
    /// методы расширения для классов реализующих IAccount
    /// </summary>
    public static class AccountExtensionMethod
    {
        /// <summary>
        /// Перевод 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static bool Transaction(this IAccount from, IAccount to, float sum){
            if (from.ReduceBalance(sum)){
                return to.IncreaseBalance(sum);
            }
            return false;
        }

        public static bool Transaction(this IAccount from, float sum){
            return from.IncreaseBalance(sum);
        }
    }
}
