using BankLibrary.Model.ClientModel;
using System;

namespace BankLibrary.Model
{
    /// <summary>
    /// Клиент без привелегий
    /// </summary>
    public class RegularClient : ClientBase
    {
        public RegularClient(Guid id,string name, string surname)
            : base(id,name, surname, ClientType.Regular) { }
    }
}
