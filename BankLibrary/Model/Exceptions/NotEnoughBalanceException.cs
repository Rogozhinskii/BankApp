using System;

namespace BankLibrary.Model.Exceptions
{
    /// <summary>
    /// Выбрасывается при попытке снятия со счета средств, больше, чем на нем имеется
    /// </summary>
    public class NotEnoughBalanceException:Exception
    {
        public NotEnoughBalanceException(string errorMessage= "there is not enough money in the account.") : base(errorMessage) { }        

        
    }
}
