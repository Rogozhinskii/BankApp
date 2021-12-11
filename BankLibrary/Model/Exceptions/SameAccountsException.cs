using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary.Model.Exceptions
{
    /// <summary>
    /// Выбрасывается, когда пытаются объединить два одинаковых счета
    /// </summary>
    public class SameAccountsException:Exception
    {
        public SameAccountsException(string msg= "The same account cannot be consolidated"):base(msg) {}
    }
}
